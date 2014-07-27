using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace CockBlockDefense
{
    public class GamePlayScreen
    {
        private Game1 game;

        Level level = new Level();
        UIStat UIstat;
        UIInfo wave;
        UItowers tower;
        forward ForwardPosition;
        pause PausePlayPosition;
        Options settingsPosition;

        Texture2D heart;
        Texture2D coin;
        WaveManager waveManager1, waveManager2;
        //Lane2 wave2;
        Player player;

        Button arrowButton;

        HUD hud;
        SoundEffect soundEffect;
       
        public GamePlayScreen(Game1 game)
        {
            this.game = game;
            

            hud = new HUD();
            hud.Font = game.Content.Load<SpriteFont>("Arial");

            LoadContent();
        }

        protected void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            Texture2D grass0 = game.Content.Load<Texture2D>("tiles/grass");
            Texture2D path1 = game.Content.Load<Texture2D>("tiles/h-path");
            Texture2D path2 = game.Content.Load<Texture2D>("tiles/v-path1");
            Texture2D path3 = game.Content.Load<Texture2D>("tiles/v-path2");
            Texture2D path4 = game.Content.Load<Texture2D>("tiles/botcorner-path1");
            Texture2D path5 = game.Content.Load<Texture2D>("tiles/botcorner-path2");
            Texture2D path6 = game.Content.Load<Texture2D>("tiles/corner-path1");
            Texture2D path7 = game.Content.Load<Texture2D>("tiles/corner-path2");
            Texture2D path8 = game.Content.Load<Texture2D>("tiles/3-waypath");
            Texture2D design9 = game.Content.Load<Texture2D>("tiles/designtile1");
            Texture2D design10 = game.Content.Load<Texture2D>("tiles/designtile2");
            Texture2D design11 = game.Content.Load<Texture2D>("tiles/designtile3");
            Texture2D design12 = game.Content.Load<Texture2D>("tiles/designtile4");
            level.AddTexture(grass0);
            level.AddTexture(path1);
            level.AddTexture(path2);
            level.AddTexture(path3);
            level.AddTexture(path4);
            level.AddTexture(path5);
            level.AddTexture(path6);
            level.AddTexture(path7);
            level.AddTexture(path8);
            level.AddTexture(design9);
            level.AddTexture(design10);
            level.AddTexture(design11);
            level.AddTexture(design12);

            Texture2D enemyTexture = game.Content.Load<Texture2D>("EnemySprites/normalEnemySide");
            Texture2D enemyTexture2 = game.Content.Load<Texture2D>("EnemySprites/normalEnemyFront");
            Texture2D towerTexture = game.Content.Load<Texture2D>("TowerSprites/chickenNormal");
            Texture2D bulletTexture = game.Content.Load<Texture2D>("TowerSprites/prjt_egg_medium");
            //wave = new Wave(0, 50, level, enemyTexture);
            //wave.Start();

            waveManager1 = new WaveManager(level, 5, enemyTexture);
            waveManager2 = new WaveManager(level, 5, enemyTexture);

            //wave2 = new Lane2(0, 50, level, enemyTexture);
            //wave2.Start();
            player = new Player(level, towerTexture, bulletTexture);

            Texture2D topBar =game.Content.Load<Texture2D>("UI/UIstat");
            SpriteFont font = game.Content.Load<SpriteFont>("Arial");

            UIstat = new UIStat(topBar, font, new Vector2(475, level.Height * 50 - 50));



            Texture2D topBar2 = game.Content.Load<Texture2D>("UI/UIinfo");
            wave = new UIInfo(topBar2, font, new Vector2(325, level.Height * 50 - 45));


            Texture2D topBar3 = game.Content.Load<Texture2D>("UI/UITowers");
            tower = new UItowers(topBar3, font, new Vector2(0, level.Height * 50 - 50));

            Texture2D pause = game.Content.Load<Texture2D>("UI/pauseButton");
            PausePlayPosition = new pause (pause, font, new Vector2(0, level.Height *2));

            Texture2D forward = game.Content.Load<Texture2D>("UI/x2Button");
            ForwardPosition = new forward(forward, font, new Vector2(0, level.Height * 2));


            Texture2D arrowNormal = game.Content.Load<Texture2D>("UI/normalTowerIcon");
   
            Texture2D arrowHover = game.Content.Load<Texture2D>("UI/normalTowerIcon");
      
            Texture2D arrowPressed = game.Content.Load<Texture2D>("UI/normalTowerIcon");

        
            arrowButton = new Button(arrowNormal, arrowHover, arrowPressed, new Vector2(50, level.Height*49-5));

            arrowButton.Clicked += new EventHandler(arrowButton_Clicked);

            Texture2D settings = game.Content.Load<Texture2D>("UI/settingUiButton");
            settingsPosition = new Options(settings, font, new Vector2(0, level.Height * 1));
        }
        private void arrowButton_Clicked(object sender, EventArgs e)
        {
            player.NewTowerType = "Normal Tower";
        }

       
        public void Update(GameTime gameTime)
        {
            List<Enemy> enemies = new List<Enemy>();

            player.Update(gameTime, waveManager1.Enemies, waveManager2.Enemies);
            arrowButton.Update(gameTime);
            waveManager1.Update(gameTime);
            waveManager2.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            level.Draw(spriteBatch);
            player.Draw(spriteBatch);
            waveManager1.Draw(spriteBatch);
            waveManager2.Draw(spriteBatch);
            UIstat.Draw(spriteBatch, player);
            wave.Draw(spriteBatch, player);
            tower.Draw(spriteBatch, player);
            PausePlayPosition.Draw(spriteBatch, player);
            ForwardPosition.Draw(spriteBatch, player);
            settingsPosition.Draw(spriteBatch, player);
            arrowButton.Draw(spriteBatch);
        }
    }
}
