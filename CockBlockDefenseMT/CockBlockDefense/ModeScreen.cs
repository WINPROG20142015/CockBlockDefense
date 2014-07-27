using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace CockBlockDefense
{
    public class ModeScreen
    {
        private Texture2D texture;
        private Game1 game;
        private KeyboardState lastState;

        private enum BState
        {
            HOVER,
            UP,
            JUST_RELEASED,
            DOWN
        }
        private const int NUMBER_OF_BUTTONS = 5,
            EASY_BUTTON_INDEX = 0,
            NORMAL_BUTTON_INDEX = 1,
            HARD_BUTTON_INDEX = 2,
            ENDLESS_BUTTON_INDEX = 3,
            BACK_BUTTON_INDEX = 4,
            BUTTON_WIDTH = 384,
            BUTTON_HEIGHT = 245;

        private Color background_color;
        private Color[] button_color = new Color[NUMBER_OF_BUTTONS];
        private Rectangle[] button_rectangle = new Rectangle[NUMBER_OF_BUTTONS];
        private BState[] button_state = new BState[NUMBER_OF_BUTTONS];
        private Texture2D[] button_texture = new Texture2D[NUMBER_OF_BUTTONS];
        private double[] button_timer = new double[NUMBER_OF_BUTTONS];
        //mouse pressed and mouse just pressed
        private bool mpressed, prev_mpressed = false;
        //mouse location in window
        private int mx, my;
        private double frame_time;

        public ModeScreen(Game1 game)
        {
            this.game = game;
            texture = game.Content.Load<Texture2D>(@"bg/modeBg");
            button_texture[EASY_BUTTON_INDEX] =
                game.Content.Load<Texture2D>(@"buttons/easyButton");
            button_texture[NORMAL_BUTTON_INDEX] =
                game.Content.Load<Texture2D>(@"buttons/normalButton");
            button_texture[HARD_BUTTON_INDEX] =
                game.Content.Load<Texture2D>(@"buttons/hardButton");
            button_texture[ENDLESS_BUTTON_INDEX] =
                game.Content.Load<Texture2D>(@"buttons/endlessButton");
            button_texture[BACK_BUTTON_INDEX] =
                game.Content.Load<Texture2D>(@"buttons/backButton");
            lastState = Keyboard.GetState();
            initialize();
        }

        protected void initialize()
        {
            // starting x and y locations to stack buttons 
            // vertically in the middle of the screen
            int[] x = new int[5]{11, 405, 11, 405, 0};
            int[] y = new int[5]{47, 47, 293, 293, 544};
            for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
            {
                button_state[i] = BState.UP;
                button_timer[i] = 0.0;
                if (i != 4)
                    button_rectangle[i] = new Rectangle(x[i], y[i], BUTTON_WIDTH, BUTTON_HEIGHT);
                else if (i == 4)
                    button_rectangle[i] = new Rectangle(x[i], y[i], 155, 56);
            }
            game.IsMouseVisible = true;
        }
        public void Update(GameTime gameTime)
        {
            // get elapsed frame time in seconds
            frame_time = gameTime.ElapsedGameTime.Milliseconds / 1000.0;

            // update mouse variables
            MouseState mouse_state = Mouse.GetState();
            mx = mouse_state.X;
            my = mouse_state.Y;
            prev_mpressed = mpressed;
            mpressed = mouse_state.LeftButton == ButtonState.Pressed;

            update_buttons();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
                spriteBatch.Draw(texture, new Vector2(0f, 0f), Color.White);

            for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
                spriteBatch.Draw(button_texture[i], button_rectangle[i], button_color[i]);
        }

        // wrapper for hit_image_alpha taking Rectangle and Texture
        Boolean hit_image_alpha(Rectangle rect, Texture2D tex, int x, int y)
        {
            return hit_image_alpha(0, 0, tex, tex.Width * (x - rect.X) /
                rect.Width, tex.Height * (y - rect.Y) / rect.Height);
        }

        // wraps hit_image then determines if hit a transparent part of image 
        Boolean hit_image_alpha(float tx, float ty, Texture2D tex, int x, int y)
        {
            if (hit_image(tx, ty, tex, x, y))
            {
                uint[] data = new uint[tex.Width * tex.Height];
                tex.GetData<uint>(data);
                if ((x - (int)tx) + (y - (int)ty) *
                    tex.Width < tex.Width * tex.Height)
                {
                    return ((data[
                        (x - (int)tx) + (y - (int)ty) * tex.Width
                        ] &
                                0xFF000000) >> 24) > 20;
                }
            }
            return false;
        }

        // determine if x,y is within rectangle formed by texture located at tx,ty
        Boolean hit_image(float tx, float ty, Texture2D tex, int x, int y)
        {
            return (x >= tx &&
                x <= tx + tex.Width &&
                y >= ty &&
                y <= ty + tex.Height);
        }

        // determine state and color of button
        void update_buttons()
        {
            for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
            {

                if (hit_image_alpha(
                    button_rectangle[i], button_texture[i], mx, my))
                {
                    button_timer[i] = 0.0;
                    if (mpressed)
                    {
                        // mouse is currently down
                        button_state[i] = BState.DOWN;
                        button_color[i] = Color.Blue;
                    }
                    else if (!mpressed && prev_mpressed)
                    {
                        // mouse was just released
                        if (button_state[i] == BState.DOWN)
                        {
                            // button i was just down
                            button_state[i] = BState.JUST_RELEASED;
                        }
                    }
                    else
                    {
                        button_state[i] = BState.HOVER;
                        button_color[i] = Color.LightBlue;
                    }
                }
                else
                {
                    button_state[i] = BState.UP;
                    if (button_timer[i] > 0)
                    {
                        button_timer[i] = button_timer[i] - frame_time;
                    }
                    else
                    {
                        button_color[i] = Color.White;
                    }
                }

                if (button_state[i] == BState.JUST_RELEASED)
                {
                    take_action_on_button(i);
                }
            }
        }
        // Logic for each button click goes here
        void take_action_on_button(int i)
        {
            //take action corresponding to which button was clicked
            switch (i)
            {
                case ENDLESS_BUTTON_INDEX:
                    game.StartGame();
                    break;
                case BACK_BUTTON_INDEX:
                    game.BackToStart();
                    break;
                default:
                    break;
            }
        }
    }
}
