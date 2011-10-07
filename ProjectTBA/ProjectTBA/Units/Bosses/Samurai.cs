using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectTBA.Misc;
using Projectiles;
using ProjectTBA.Projectiles;
using System.Diagnostics;

namespace ProjectTBA.Units
{
    public class Samurai : Unit
    {

        private enum MovementState
        {
            idle, moveLeft, moveRight, jumping, falling, attacking
        }
        private MovementState currentState;

        private enum PatrolState
        {
            idle, left, right, jumpLeft, jumpRight
        }
        private PatrolState patrolState = PatrolState.left;

        private int jumpSpeed = 4;
        private int maxJumpHeight;
        private int jumpedHeight = 0;
        private Boolean isChasing = true;
        private int patrolJumpHeight = 200;
        private int patrolJumpSpeed = 8;
        private Demon target;
        /// <summary>
        /// 0 = left
        /// 1 = right
        /// </summary>
        private int direction = 0;

        public Samurai(float x, float y)
            : base(x, y)
        {
            this.health = 500;
            game.AddUnit(this);
            target = game.player;
            this.texture = AkumaContentManager.samuraiTempTex;
            this.movementSpeed = 6f;
            maxJumpHeight = game.currentLevel.levelHeight - texture.Height;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, GetDrawLocation(), new Rectangle((73 * direction), 0, 56, 100), Color.White, 0f, Vector2.Zero, 1f, spriteEffect, 0.1f);
        }

        public Vector2 GetDrawLocation()
        {
            Vector2 loc = new Vector2();
            loc.X = location.X - game.currentLevel.offset.X;
            loc.Y = location.Y - game.currentLevel.offset.Y;
            return loc;
        }

        public override void Update(GameTime gameTime)
        {
            if (this.location.X - game.player.location.X < 50 || this.location.X - game.player.location.X > -50)
            {
                if (this.GetRectangle().Intersects(game.player.GetRectangle()))
                {
                    game.player.Hit(this);
                }
            }

            if (isChasing)
            {
                Chase();
            }
            else
            {
                Move(currentState);
            }

            if (!jumping && !falling && 
                this.currentState != MovementState.jumping && 
                this.currentState != MovementState.falling && 
                this.currentState != MovementState.attacking)
            {
                int chance = game.currentLevel.random.Next(100);

                if (chance < 2)
                {
                    if (currentState == MovementState.moveLeft)
                    {
                        patrolState = PatrolState.left;
                    }
                    else if (currentState == MovementState.moveRight)
                    {
                        patrolState = PatrolState.right;
                    }
                    else if (patrolState == PatrolState.left)
                    {
                        currentState = MovementState.moveLeft;
                    }
                    else if (patrolState == PatrolState.right)
                    {
                        currentState = MovementState.moveRight;
                    }
                    isChasing = !isChasing;
                }
            }
        }

        public override void Attack()
        {
        }

        private void Move(MovementState state)
        {
            switch (state)
            {
                case MovementState.idle:
                    this.currentState = MovementState.moveLeft;
                    break;
                case MovementState.moveLeft:
                    
                    if (this.location.X < 10)
                    {
                        this.currentState = MovementState.jumping;
                        spriteEffect = SpriteEffects.FlipHorizontally;
                    }
                    else
                    {
                        this.location.X -= movementSpeed;
                    }
                    break;
                case MovementState.moveRight:
                    if (this.location.X > game.currentLevel.levelWidth - 56)
                    {
                        this.currentState = MovementState.jumping;
                        spriteEffect = SpriteEffects.None;
                    }
                    else
                    {
                        this.location.X += movementSpeed;
                    }
                    break;
                case MovementState.jumping:
                    if (jumpedHeight > maxJumpHeight)
                    {
                        //peak jump
                        this.currentState = MovementState.attacking;
                        //this.currentState = MovementState.falling;
                        if (this.location.X < 100)
                        {
                            game.currentLevel.projectiles.AddLast(new Shuriken(location.X, location.Y, 5, 5));
                            game.currentLevel.projectiles.AddLast(new Shuriken(location.X, location.Y, 7, 5));
                            game.currentLevel.projectiles.AddLast(new Shuriken(location.X, location.Y, 10, 5));
                        }
                        else
                        {
                            game.currentLevel.projectiles.AddLast(new Shuriken(location.X, location.Y, -5, 5));
                            game.currentLevel.projectiles.AddLast(new Shuriken(location.X, location.Y, -7, 5));
                            game.currentLevel.projectiles.AddLast(new Shuriken(location.X, location.Y, -10, 5));
                        }
                    }
                    else
                    {
                        this.location.Y -= jumpSpeed;
                        jumpedHeight += jumpSpeed;
                    }
                    break;
                case MovementState.falling:
                    if (jumpedHeight <= 0)
                    {
                        if (this.location.X < 10)
                        {
                            this.currentState = MovementState.moveRight;
                        }
                        else if (this.location.X > game.currentLevel.levelWidth - 56)
                        {
                            this.currentState = MovementState.moveLeft;
                        }
                    }
                    else
                    {
                        this.location.Y += jumpSpeed;
                        jumpedHeight -= jumpSpeed;
                    }
                    break;
                case MovementState.attacking:
                    for (int i = 0; i < game.currentLevel.projectiles.Count; i++)
                    {
                        if (!game.currentLevel.projectiles.ElementAt(i).isDead)
                        {
                            return;
                        }
                    }
                    this.currentState = MovementState.falling;
                    break;
                default: break;

            }
        }

        private void Chase()
        {
            ChaseMovement(patrolState);
        }

        private void ChaseMovement(PatrolState patrolState)
        {
            switch (patrolState)
            {
                case PatrolState.idle:
                    this.patrolState = PatrolState.right;
                    break;
                case PatrolState.left:
                    if (this.location.X < 10 && this.location.Y <= game.currentLevel.levelHeight)
                    {
                        spriteEffect = SpriteEffects.FlipHorizontally;
                        this.patrolState = PatrolState.right;
                        return;
                    }
                    else
                    {
                        this.location.X -= movementSpeed;
                    }

                    if (target.location.X + target.textureWidth >= this.location.X - 10 &&
                        this.location.Y == 300)
                    {
                        jumping = true;
                    }

                    PatrolJump();
                    break;
                case PatrolState.right:

                    if (this.location.X + texture.Width > game.currentLevel.levelWidth && this.location.Y >= 300)
                    {
                        spriteEffect = SpriteEffects.None;
                        this.patrolState = PatrolState.left;
                        return;
                    }
                    else
                    {
                        this.location.X += movementSpeed;
                    }

                    if (this.location.X + this.texture.Width <= target.location.X - 10 &&
                        this.location.Y == 300)
                    {
                        jumping = true;
                    }

                    PatrolJump();
                    break;
                default: break;


            }
        }

        private void PatrolJump()
        {
            if (jumping)
            {
                if (jumpedHeight > patrolJumpHeight)
                {
                    falling = true;
                    jumping = false;
                    return;
                }
                else
                {
                    this.location.Y -= patrolJumpSpeed;
                    jumpedHeight += patrolJumpSpeed;
                }
            }
            else if (falling)
            {
                if (jumpedHeight == 0)
                {
                    falling = false;
                }
                else
                {
                    this.location.Y += patrolJumpSpeed;
                    jumpedHeight -= patrolJumpSpeed;
                }
            }
        }

        public override void Die()
        {
            game.RemoveUnit(this);
            game.currentLevel.finalBoss = true;
            game.Reset();
        }

        public override Rectangle GetRectangle()
        {
            return new Rectangle((int)location.X - (int)Game1.GetInstance().currentLevel.offset.X,
                (int)location.Y, 56, texture.Height);
        }

        public override Rectangle GetFeetHitbox()
        {
            return new Rectangle((int)location.X + 37 - (int)game.currentLevel.offset.X, (int)location.Y + 99, 36, 1);
        }
    }
}
