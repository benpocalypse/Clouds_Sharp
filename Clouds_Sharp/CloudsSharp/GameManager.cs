using System;
using SDL2;
using System.Collections.Generic;

namespace CloudsSharp
{
	public class GameManager
	{
        /*
		 gobject *enemies;
         gobject player;
         gobject *bullets;
         gobject *ebullets1;
         gobject *ebullets2;
         gobject *ebullets3;
         gobject *ebullets4;
         gplayer playerstats;
         gmap game_map;
         gtexmanager texman;
         grendermanager gamerenderer;
         glogicmanager gamelogic;
         ginputhandler gameinput;
         gpowerupmanager gpowerup;
         gsoundmanager smanager;
         bool running;
         SDL_Event gameevent;
         int gamestate;
	     int fpscount;
	     int cinemacount;
	  */
        private List<GObject> enemies;
        private GObject player;
        private List<GObject> bullets;
        private List<GObject> ebullets1;
        private List<GObject> ebullets2;
        private List<GObject> ebullets3;
        private List<GObject> ebullets4;
        private Player playerstats;
        private GameMap gamemap;
        private TextureManager texman;
        private RenderManager gamerenderer;
        private LogicManager gamelogic;

        public bool bRunning = true;

		public enum Game_State
		{
			GameOver,
			Playing,
			Paused,
			Title,
			Cinema,
			LevelOver
		};

		public Game_State gState = Game_State.Title;

		private SDL.SDL_Event gEvent;

		public GameManager()
		{
			gState = Game_State.Title;
			bRunning = true;

			/*
			  srand(time(NULL));
			  if(gamerenderer.init() && smanager.init())
			  {
			    gamelogic.init();
			    atexit(SDL_Quit);
				  fpscount = 0;
				  cinemacount = 0;
				  playerstats.init();
				  gamestate = TITLE;
				  smanager.play_music("data/music/title.ogg");
				  running = true;
			    return 1;
			  }
			  else
			  {
			    running = false;
			    return 0;
			  }
			  */
		}

		public void Update()
		{
			switch(gState)
			{
				case Game_State.Title:
					while(SDL.SDL_PollEvent(out gEvent) == 1){}
					break;
			}

			/*

			//TITLE
			if (gamestate == TITLE)
			{
				while (SDL_PollEvent(&gameevent)) { }

				gamerenderer.drawscreen("data/misc/logo.png");
				gamerenderer.print("Press Space to start.", 80, 200, 21);
				gamerenderer.flip();
				gameinput.get_input();

				if (gameinput.keys == SPACE)
				{
					smanager.stop_music();
					smanager.play_music("data/music/cinema1.ogg");
					gamestate = CINEMA;
					while (gameinput.keys == SPACE) { gameinput.get_input(); }
					//load("data/maps/test.map");
				}

				//If the X is clicked, exit the game.
				if (gameevent.type == SDL_QUIT)
				{
					smanager.stop_music();
					running = false;
					SDL_Quit();
				}
			}

			//CINEMA
			if (gamestate == CINEMA)
			{
				while (SDL_PollEvent(&gameevent)) { }
				switch (cinemacount)
				{
					case 0:
						gamerenderer.drawscreen("data/misc/cinema1.png");
						gamerenderer.print(" ALEX IS JUST LIKE ANY NORMAL KID:  ", 14, 182, 36);
						gamerenderer.print("HE HATES SCHOOL, HOMEWORK, TEACHERS,", 14, 192, 36);
						gamerenderer.print("AND WATCHING TV. ALL HE CAN THINK   ", 14, 202, 36);
						gamerenderer.print("ABOUT IS HOW BEAUTIFUL CLOUDS ARE.  ", 14, 212, 36);
						gamerenderer.print("(Press Space to continue)", 55, 230, 25);
						break;
					case 1:
						gamerenderer.drawscreen("data/misc/cinema2.png");
						gamerenderer.print(" ALEX ALWAYS LIKED USING HIS        ", 14, 182, 36);
						gamerenderer.print("IMAGINATION. SO TODAY, ALEX DECIDED ", 14, 192, 36);
						gamerenderer.print("THAT THE WORLD SHOULD BE DIFFERENT. ", 14, 202, 36);
						gamerenderer.print("SO HE USED HIS MIND TO CHANGE IT... ", 14, 212, 36);
						gamerenderer.print("(Press Space to continue)", 55, 230, 25);
						break;
				}

				gamerenderer.flip();
				gameinput.get_input();

				if (gameinput.keys == SPACE)
				{
					while (gameinput.keys == SPACE) { gameinput.get_input(); }
					cinemacount++;
				}

				if (cinemacount == 2) //start the first level
				{
					smanager.stop_music();
					gamestate = PLAYING;
					load("data/maps/test.map");
				}

				//If the X is clicked, exit the game.
				if (gameevent.type == SDL_QUIT)
				{
					smanager.stop_music();
					running = false;
					SDL_Quit();
				}
			}

			//PLAYING
			if (gamestate == PLAYING)
			{
				fpscount++;
				static char buffer[4] = "000";
				bool enemiesactive = false;

				//mandatory check for events
				while (SDL_PollEvent(&gameevent)) { }

				//draw everything
				gamerenderer.drawmap(&game_map, &texman, gamelogic.camera_pos, gamelogic.camera_pos_small);
				gamerenderer.drawbullets(bullets, &texman, gamelogic.camera_pos, gamelogic.camera_pos_small);
				gamerenderer.drawbullets(ebullets1, &texman, gamelogic.camera_pos, gamelogic.camera_pos_small);
				gamerenderer.drawbullets(ebullets2, &texman, gamelogic.camera_pos, gamelogic.camera_pos_small);
				gamerenderer.drawbullets(ebullets3, &texman, gamelogic.camera_pos, gamelogic.camera_pos_small);
				gamerenderer.drawbullets(ebullets4, &texman, gamelogic.camera_pos, gamelogic.camera_pos_small);
				gamerenderer.drawpowerup(&gpowerup, &texman);
				gamerenderer.drawenemies(enemies, &texman, game_map.num_enemies, gamelogic.camera_pos, gamelogic.camera_pos_small);
				gamerenderer.drawplayer(&player, &texman, gamelogic.camera_pos, gamelogic.camera_pos_small);
				gamerenderer.drawexplosion(gamelogic.explosion);
				gamerenderer.drawhud(&player, &playerstats);

				//draw our FPS onscreen if defined in globals.h
				if (SHOWFPS)
				{
					gamerenderer.print("FPS:", 265, 230, 4);
					gamerenderer.print(buffer, 295, 230, 3);
				}

				//Check for collisions between player/enemies/bullets
				for (int i = 0; i < game_map.num_enemies; i++)
				{
					//check for player <-> enemy collisions
					if (enemies[i].active)
					{
						enemiesactive = true;
						if (player.active)
							if (gamelogic.collision(&player, &enemies[i], 14))
								gamelogic.add_explosion(&enemies[i]);
					}

					//do the bullet collisions
					for (int j = 0; j < MAXBULLETS; j++)
					{
						if ((bullets[j].active) && (enemies[i].active))
							if (enemies[i].ai_type != 5) //If this isn't a boss, do normal collision.
							{
								if (gamelogic.collision(&enemies[i], &bullets[j], 12))
								{
									smanager.play_sound(0);
									if (enemies[i].health <= 0)
									{
										smanager.play_sound(1);
										gamelogic.add_explosion(&enemies[i]);
										gpowerup.add_powerup(16 * (enemies[i].x - gamelogic.camera_pos) + enemies[i].small_x,
														   16 * (enemies[i].y) + enemies[i].small_y);

										playerstats.score += 5;
									}
								}
							}
							else
							if (enemies[i].ai_type == 5) //If this is a boss...
							{
								if (gamelogic.collision(&enemies[i], &bullets[j], 14, 20, 5))
								{
									//DO OVERLOADED COLLISION DETECTION W/ OFFSET
									smanager.play_sound(0);
									if (enemies[i].health <= 0)
									{
										smanager.play_sound(1);
										gamelogic.add_explosion(&enemies[i]);
										gpowerup.add_powerup(16 * (enemies[i].x - gamelogic.camera_pos) + enemies[i].small_x,
														   16 * (enemies[i].y) + enemies[i].small_y);

										playerstats.score += 5;
									}
								}
							}

						if ((ebullets1[j].active))
							if (player.active)
								gamelogic.collision(&player, &ebullets1[j], 12); //CHANGE RADIUS!!

						if ((ebullets2[j].active))
							if (player.active)
								gamelogic.collision(&player, &ebullets2[j], 12); //EVENTUALLY

						if ((ebullets3[j].active))
							if (player.active)
								gamelogic.collision(&player, &ebullets3[j], 12);

						if ((ebullets4[j].active))
							if (player.active)
								gamelogic.collision(&player, &ebullets4[j], 12);
					}
				}

				//Decide if we're at the end of level
				if (((gamelogic.camera_pos + 21) >= game_map.width) && (!enemiesactive))
				{
					gamestate = LEVELOVER;
				}
				enemiesactive = true;

				//Check for collisions between player/powerups
				for (int i = 0; i < MAXPOWERUPS; i++)
				{
					if (gpowerup.powerups[i].active)
						if (player.active)
							if (gamelogic.collision(&player, &gpowerup.powerups[i], 10, gamelogic.camera_pos))
							{
								smanager.play_sound(2);
								playerstats.add_power();
								playerstats.score += 2;
							}
				}

				//check to see if we're dead, and if we are, reset some things
				if ((!player.active) && (playerstats.delay == 0))
				{
					gamelogic.add_explosion(&player);
					player.x = gamelogic.camera_pos;
					player.small_x = gamelogic.camera_pos_small;
					player.y = 7;
					player.small_y = 0;
					playerstats.lives--;
					playerstats.power = 0;
					playerstats.delay++;
					if (playerstats.lives < 0)
						gamestate = GAMEOVER;
				}

				//Delay before respawing the player if the player is dead
				if ((!player.active) && (playerstats.delay > RESPAWNTIME))
				{
					player.health = PLAYERBASEHEALTH;
					player.active = true;
					playerstats.delay = 0;
				}

				//If the X is clicked, exit the game.
				if (gameevent.type == SDL_QUIT)
				{
					unload();
					running = false;
					SDL_Quit();
				}

				//handle 30Hz logic update
				if (gamelogic.logic_update)
				{
					//FPS Counter
					if ((gamelogic.frame_counter >= 29))
					{
						sprintf(buffer, "%d", fpscount);
						fpscount = 0;
					}

					//If the player is dead, delay their respawn.
					if (!player.active)
						playerstats.delay++;

					//Do some misc things
					//like only move the powerups if we're not at the end of a level
					if ((gamelogic.camera_pos + 21) <= game_map.width)
						gpowerup.update();

					gamelogic.update_explosions();
					gamelogic.update(&gameinput, &game_map, &player);
					gamerenderer.flip();
					player.shot_timer++;

					//check for active enemies and let them think
					for (int i = 0; i < game_map.num_enemies; i++)
					{
						gamelogic.check_active(&enemies[i]);
						if (enemies[i].active)
						{
							if (enemies[i].bullet_type == 1)
								gamelogic.think_enemy(&enemies[i], ebullets1, &player, &game_map);

							if (enemies[i].bullet_type == 2)
								gamelogic.think_enemy(&enemies[i], ebullets2, &player, &game_map);

							if (enemies[i].bullet_type == 3)
								gamelogic.think_enemy(&enemies[i], ebullets3, &player, &game_map);

							if (enemies[i].bullet_type == 4)
								gamelogic.think_enemy(&enemies[i], ebullets4, &player, &game_map);

							gamelogic.move_enemy(&enemies[i]);
						}
					}

					//handle player firing and powerups
					gameinput.get_input();
					if ((gameinput.fire) && (player.shot_timer > PLAYERFIRINGRATE) && (player.active))
					{
						if ((playerstats.power >= (MAXPOWER / 4)) && (playerstats.power < (MAXPOWER / 2)))
						{
							gamelogic.add_bullet(bullets, &player, 7, 0);
							gamelogic.add_bullet(bullets, &player, 5, 2);
						}
						else
						//if((playerstats.power >= (MAXPOWER/2)) && (playerstats.power < ((MAXPOWER*3)/4)))
						if ((playerstats.power >= (MAXPOWER / 2)) && (playerstats.power < MAXPOWER))
						{
							gamelogic.add_bullet(bullets, &player, 5, -2);
							gamelogic.add_bullet(bullets, &player, 7, 0);
							gamelogic.add_bullet(bullets, &player, 5, 2);
						}
						else
						if (playerstats.power >= MAXPOWER)
						{
							gamelogic.add_bullet(bullets, &player, 5, -2);
							gamelogic.add_bullet(bullets, &player, 6, -1);
							gamelogic.add_bullet(bullets, &player, 7, 0);
							gamelogic.add_bullet(bullets, &player, 6, 1);
							gamelogic.add_bullet(bullets, &player, 5, 2);
						}
						else
							gamelogic.add_bullet(bullets, &player, 7, 0);
					}

					if (player.active)
						gamelogic.move_player(&gameinput, &game_map, &player);

					//check for active bullets and let them move
					for (int i = 0; i < MAXBULLETS; i++)
					{
						gamelogic.check_active(&bullets[i]);
						gamelogic.check_active(&ebullets1[i]);
						gamelogic.check_active(&ebullets2[i]);
						gamelogic.check_active(&ebullets3[i]);
						gamelogic.check_active(&ebullets4[i]);
					}
					gamelogic.move_bullet(bullets);
					gamelogic.move_bullet(ebullets1);
					gamelogic.move_bullet(ebullets2);
					gamelogic.move_bullet(ebullets3);
					gamelogic.move_bullet(ebullets4);

					//check to see if the game needs to be paused or not
					if (gameinput.keys == ESCAPE)
						gamestate = PAUSED;
				}
			}

			//PAUSED
			if (gamestate == PAUSED)
			{
				gameinput.get_input();
				gamerenderer.print("PAUSED.", 130, 110, 7);
				gamerenderer.print("Press Y to Quit,", 90, 120, 16);
				gamerenderer.print("or N to Continue.", 90, 130, 17);
				gamerenderer.flip();

				if (gameevent.type == SDL_QUIT)
				{
					unload();
					running = false;
					SDL_Quit();
				}

				if (gameinput.keys == NO)
					gamestate = PLAYING;

				if (gameinput.keys == YES)
				{
					gamestate = GAMEOVER;
				}
			}

			//GAMEOVER
			if (gamestate == GAMEOVER)
			{
				unload();
				running = false;
				SDL_Quit();
			}

			//LEVELOVER
			if (gamestate == LEVELOVER)
			{
				unload();
				running = false;
				SDL_Quit();
			}

			//Free up some CPU time
			SDL_Delay(1);

			*/
		}

		public int GetNumberOfEnemies()
		{
			int iTemp = 0;

			/*
			for(int i = 0; i < gMap.Width;i++)
			{
				for(int j = 0; j < gMap.Height; j++)
				{
					if(gMap.Data[0][i][j] != 0)
						iTemp++;
				}	
			}
			*/

			return iTemp;
		}

		public void Load(string sMapName)
		{
			/*
			//A good idea would be to have a loading screen or a progress
			//meter displayed throughout this function, because its
			//possible that large levels and numbers of enemies can take
			//awhile to load.
			game_map.open(map);
			game_map.num_enemies = get_num_enemies();

			//number of enemies + number of textures + bullets + player + powerup = total textures
			texman.init(game_map.num_enemies + game_map.num_textures + 5 + 1 + 1);

			//Now add all the textures to the texmanager list and get
			//their texture IDs.

			//First start with the tile textures;
			for (int i = 0; i < game_map.num_textures; i++)
			{
				std::string buffer;
				buffer.clear();
				buffer = "data/tiles/";
				buffer += game_map.tex_names[i];
				if (texman.add(buffer.c_str()) == -1)
				{
					printf("Error loading texture: %s\n", buffer.c_str());
				}
			}

			//Then we'll do the enemy textures, this is a bit
			//hackish and tricky, but its easier than having
			//config files for every different enemy. Change
			//this later to something more elegant.
			enemies = new gobject[game_map.num_enemies];
			int temp = 0;
			int temp_tex = 0;
			for (int i = 0; i < game_map.width; i++)
			{
				for (int j = 0; j < game_map.height; j++)
				{
					if (game_map.data[0][i][j] != 0)
					{
						switch (game_map.data[0][i][j])
						{
							case 1:
								temp_tex = texman.add("data/sprites/enemy1.png");
								enemies[temp].init(0, 2, 4, temp_tex, i, j, 0, 0, 0, 0);
								enemies[temp].ai_type = 1;
								enemies[temp].angle = 0;
								enemies[temp].player = 0;
								enemies[temp].bullet_type = 1;
								temp++;
								break;

							case 2:
								temp_tex = texman.add("data/sprites/enemy2.png");
								enemies[temp].init(0, 4, 2, temp_tex, i, j, 0, 0, 0, 0);
								enemies[temp].ai_type = 2;
								enemies[temp].player = 0;
								enemies[temp].angle = 0;
								enemies[temp].bullet_type = 2;
								temp++;
								break;

							case 3:
								temp_tex = texman.add("data/sprites/enemy3.png");
								enemies[temp].init(0, 3, 6, temp_tex, i, j, 0, 0, 0, 0);
								enemies[temp].ai_type = 3;
								enemies[temp].player = 0;
								enemies[temp].angle = 0;
								enemies[temp].bullet_type = 3;
								temp++;
								break;

							case 4:
								temp_tex = texman.add("data/sprites/enemy3.png");
								enemies[temp].init(0, 3, 3, temp_tex, i, j, 0, 0, 0, 0);
								enemies[temp].ai_type = 4;
								enemies[temp].player = 0;
								enemies[temp].angle = 0;
								enemies[temp].bullet_type = 3;
								temp++;
								break;

							case 5:
								temp_tex = texman.add("data/sprites/lunchlady.png");
								enemies[temp].init(0, 3, 30, temp_tex, i, j, 0, 0, 0, 0);
								enemies[temp].ai_type = 5;
								enemies[temp].player = 0;
								enemies[temp].angle = 0;
								enemies[temp].bullet_type = 4;
								temp++;
								break;

							default:
								temp_tex = texman.add("data/sprites/enemy0.png");
								enemies[temp].init(0, 1, 1, temp_tex, i, j, 0, 0, 0, 0);
								enemies[temp].ai_type = 0;
								enemies[temp].player = 0;
								temp++;
								break;
						}
					}
				}
			}

			//Now do the bullets.
			//For now, all the bullets use the same graphic.
			int temp_texid = texman.add("data/sprites/bullet1.png");
			bullets = new gobject[MAXBULLETS];
			for (int i = 0; i < MAXBULLETS; i++)
			{
				bullets[i].init(0, 1, 1, temp_texid, -10, -10, 0, 0, 0, 0);
			}

			temp_texid = texman.add("data/sprites/bullet2.png");
			ebullets1 = new gobject[MAXBULLETS];
			for (int i = 0; i < MAXBULLETS; i++)
			{
				ebullets1[i].init(0, 1, 1, temp_texid, -10, -10, 0, 0, 0, 0);
			}

			temp_texid = texman.add("data/sprites/bullet3.png");
			ebullets2 = new gobject[MAXBULLETS];
			for (int i = 0; i < MAXBULLETS; i++)
			{
				ebullets2[i].init(0, 1, 1, temp_texid, -10, -10, 0, 0, 0, 0);
			}

			temp_texid = texman.add("data/sprites/bullet4.png");
			ebullets3 = new gobject[MAXBULLETS];
			for (int i = 0; i < MAXBULLETS; i++)
			{
				ebullets3[i].init(0, 1, 1, temp_texid, -10, -10, 0, 0, 0, 0);
			}

			temp_texid = texman.add("data/sprites/bullet5.png");
			ebullets4 = new gobject[MAXBULLETS];
			for (int i = 0; i < MAXBULLETS; i++)
			{
				ebullets4[i].init(0, 1, 1, temp_texid, -10, -10, 0, 0, 0, 0);
			}

			//do the powerup
			gpowerup.init(texman.add("data/sprites/crystal.png"));

			//Now all thats left is the player.
			//Put him on the complete lefthand side of the map, halfway up.
			//Make sure the lefthand row of your collision layer doesn't
			//have a tile in it.
			player.init(true, PLAYERBASEDAMAGE, PLAYERBASEHEALTH,
						texman.add("data/sprites/player.png"),
						0, game_map.height / 2, 0, 0, 0, 0);
			player.player = 1;

			//initialize the explosions
			for (int i = 0; i < MAXEXPLOSIONS; i++)
			{
				gamelogic.explosion[i].init(-100, -100);
				gamelogic.explosion[i].active = false;
			}

			//Hack transparency for all the textures that the texture
			//manager holds. Magic pink is our transparent color.
			for (int i = 0; i < texman.total_textures; i++)
			{
				SDL_SetColorKey(texman.tex_list[i], SDL_SRCCOLORKEY,
								SDL_MapRGB(texman.tex_list[i]->format, 255, 0, 255));
			}

			//Handle loading/caching of all the sounds
			std::string buffer;
			buffer.clear();
			buffer = "data/music/";
			buffer += game_map.music;

			smanager.load_sound("data/sounds/hit1.wav");
			smanager.load_sound("data/sounds/explosion1.wav");
			smanager.load_sound("data/sounds/pickup1.wav");
			smanager.play_music(buffer.c_str());
			*/
		}

		public void Unload()
		{
			// This is probably not needed.
		}
	}
}
