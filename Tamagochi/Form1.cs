using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tamagochi
{
    partial class Form1 : Form
    {
        PictureBox[] queue;
        public Form1()
        {
            InitializeComponent();

            new Settings();

            queue = new PictureBox[]
            {
                pbQueue1,
                pbQueue2,
                pbQueue3,
                pbQueue4,
                pbQueue5,
                pbQueue6
            };

            gameTimer.Interval = 1000 / Settings.speed;
            gameTimer.Start();

            queue_timer.Interval = 1000 / Settings.queueSpeed;
            queue_timer.Start();

            init_game();
        }

        private void init_scale(Label lbl_cur, Label lbl_max, Scale scale)
        {
            lbl_cur.Text = scale.current_value.ToString();
            lbl_max.Text = scale.max_value.ToString();
        }

        private void init_game()
        {
            new Settings();
            init_scale(lblEatCur, lblEatMax, Settings.eat);
            init_scale(lblSleepCur, lblSleepMax, Settings.sleep);
            init_scale(lblHappyCur, lblHappyMax, Settings.happy);
            init_scale(lblClearCur, lblClearMax, Settings.clear);
            init_scale(lblHPCur, lblHPMax, Settings.hp);

            lblGameOver.Visible = false;
        }

        bool is_die()
        {
            int cur_life = (
                Settings.eat.current_value +
                Settings.clear.current_value +
                Settings.sleep.current_value +
                Settings.happy.current_value
                ) / 4;
            Settings.hp.current_value = cur_life;
            if (
                Settings.eat.current_value == 0 ||
                Settings.clear.current_value == 0 ||
                Settings.sleep.current_value == 0 ||
                Settings.happy.current_value == 0
                )
            {
                Settings.hp.current_value = 0;
                return true;
            }
            return false;
        }

        private void set_scales()
        {
            lblEatCur.Text = Settings.eat.current_value.ToString();
            lblClearCur.Text = Settings.clear.current_value.ToString();
            lblHappyCur.Text = Settings.happy.current_value.ToString();
            lblSleepCur.Text = Settings.sleep.current_value.ToString();
            lblHPCur.Text = Settings.hp.current_value.ToString();
        }

        private void game_over_actions()
        {
            pbImage.BackgroundImage = Properties.Resources.die;
            lblHPCur.Text = "0";
            lblGameOver.Visible = true;
            btnEat.Enabled = false;
            btnHappy.Enabled = false;
            btnClear.Enabled = false;
            btnSleep.Enabled = false;
            btnAction.Enabled = false;
        }
        void generate_action(Random random)
        {
            List<Action> list = new List<Action>() { dec_eat, dec_clear, dec_happy, dec_sleep };
            int index = random.Next(list.Count);
            list[index]();
        }

        void dec_eat()
        {
            Settings.eat.sub_value(Settings.sub);
            set_scales();
            Settings.is_gameover = is_die();
        }

        void dec_sleep()
        {
            Settings.sleep.sub_value(Settings.sub);
            set_scales();
            Settings.is_gameover = is_die();
        }
        void dec_clear()
        {
            Settings.clear.sub_value(Settings.sub);
            set_scales();
            Settings.is_gameover = is_die();
        }
        void dec_happy()
        {
            Settings.happy.sub_value(Settings.sub);
            set_scales();
            Settings.is_gameover = is_die();
        }

        private void btnEat_Click(object sender, EventArgs e)
        {
            //eating();
            Settings.queue.Enqueue(
                new KeyValuePair<Actions, Image>(
                    Actions.Eat,
                    Properties.Resources._7
                    )
                );
        }

        private void eating()
        {
            Settings.eat.add_value(Settings.add);
            Settings.clear.sub_value(Settings.sub);
            set_scales();
            Settings.is_gameover = is_die();
        }

        private void btnSleep_Click(object sender, EventArgs e)
        {
            //sleeping();
            Settings.queue.Enqueue(
                new KeyValuePair<Actions, Image>(
                    Actions.Sleep,
                    Properties.Resources._8
                    )
                );
        }

        private void sleeping()
        {
            Settings.sleep.add_value(Settings.add);
            Settings.eat.sub_value(Settings.sub);
            set_scales();
            Settings.is_gameover = is_die();
        }

        private void btnHappy_Click(object sender, EventArgs e)
        {
            //playing();
            Settings.queue.Enqueue(
                new KeyValuePair<Actions, Image>(
                    Actions.Game,
                    Properties.Resources._9
                    )
                );
        }

        private void playing()
        {
            Settings.happy.add_value(Settings.add);
            Settings.sleep.sub_value(Settings.sub);
            set_scales();
            Settings.is_gameover = is_die();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //showering();
            Settings.queue.Enqueue(
                new KeyValuePair<Actions, Image>(
                    Actions.Clear,
                    Properties.Resources._10
                    )
                );
        }

        private void showering()
        {
            Settings.clear.add_value(Settings.add);
            Settings.happy.sub_value(Settings.sub);
            set_scales();
            Settings.is_gameover = is_die();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (Settings.is_gameover)
            {
                game_over_actions();
            }
            else
            {
                Random random = new Random();
                int is_action = random.Next(0, 2);
                if (is_action == 1)
                {
                    generate_action(random);
                }
            }
        }

        private void queue_timer_Tick(object sender, EventArgs e)
        {
            var elements = Settings.queue.Elements;
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] != null)
                {
                    var element = elements[i];                                   //может быть null
                    var checked_element = (KeyValuePair<Actions, Image>)element; //НЕ может быть null
                    queue[i].Image = checked_element.Value;
                }
                else
                {
                    queue[i].Image = null;
                }
            }
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            var element = Settings.queue.Dequeue();
            if (element == null)
                return;

            var checked_element = (KeyValuePair<Actions, Image>)element;
            switch (checked_element.Key)
            {
                case Actions.Eat:
                    eating();
                    break;
                case Actions.Clear:
                    showering();
                    break;
                case Actions.Game:
                    playing();
                    break;
                case Actions.Sleep:
                    sleeping();
                    break;
            }

        }
    }
}
