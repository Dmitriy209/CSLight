using System;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string ButtonAttack = "1";
            const string ButtonFireBall = "2";
            const string ButtonExplosion = "3";
            const string ButtonHeal = "4";

            Random random = new Random();
            int lowLimitRandom = 1;
            int highLimitRandom = 5;

            int hitPointsBoss = 100;
            int damageBoss;

            int maxHitPointsPlayer = 10;
            int maxManaPlayer = 10;
            int hitPointsPlayer = 10;
            int manaPlayer = 10;
            int damagePlayer = 10;
            int damageFireBall = 20;
            int manaFireBall = 3;
            int damageExplosion = 30;
            int heal = 5;
            int pointsHeal = 3;

            string newTurn = null;

            while (hitPointsPlayer > 0 && hitPointsBoss > 0)
            {
                damageBoss = random.Next(lowLimitRandom, highLimitRandom + 1);
                Console.WriteLine($"Босс наносит вам {damageBoss} урона\n" +
                    "У вас осталось: " + (hitPointsPlayer -= damageBoss));

                if (hitPointsPlayer > 0 && hitPointsBoss > 0)
                {
                    Console.WriteLine("Ваш ход:");
                    string lastTurn = newTurn;
                    newTurn = Console.ReadLine();

                    switch (newTurn)
                    {
                        case ButtonAttack:
                            hitPointsBoss -= damagePlayer;
                            break;

                        case ButtonFireBall:
                            if (manaPlayer - manaFireBall > 0)
                            {
                                manaPlayer -= manaFireBall;
                                hitPointsBoss -= damageFireBall;
                            }

                            break;

                        case ButtonExplosion:

                            if (lastTurn == ButtonFireBall)
                            {
                                hitPointsBoss -= damageExplosion;
                            }
                            else
                            {
                                Console.WriteLine("В прошлом ходу вы не использовали Огненный шар," +
                                    "поэтому не можете использовать взрыв и пропускаете ход.");
                            }

                            break;

                        case ButtonHeal:

                            if (pointsHeal > 0)
                            {
                                hitPointsPlayer += heal;

                                if (hitPointsPlayer > maxHitPointsPlayer)
                                {
                                    hitPointsPlayer = maxHitPointsPlayer;
                                }

                                manaPlayer += heal;

                                if (manaPlayer > maxManaPlayer)
                                {
                                    manaPlayer = maxManaPlayer;
                                }

                                pointsHeal--;
                                Console.WriteLine("У вас закончился подорожник. Вы больше не можете хилится.");
                            }

                            break;

                        default:
                            Console.WriteLine("Вы ошеломлены мощью BossOfTheGym и пропускаете ход.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Вы умерли");
                    break;
                }
            }

            if (hitPointsBoss > 0)
            {
                Console.WriteLine("Вы проиграли.");
                Console.WriteLine("hitPointsPlayer = " + hitPointsPlayer);
                Console.WriteLine("hitPointsBoss = " + hitPointsBoss);
            }
            else if (hitPointsPlayer > 0)
            {
                Console.WriteLine("Вы выиграли");
                Console.WriteLine("hitPointsPlayer = " + hitPointsPlayer);
                Console.WriteLine("hitPointsBoss = " + hitPointsBoss);
            }
            else if (hitPointsPlayer == hitPointsBoss)
            {
                Console.WriteLine("Ничья");
                Console.WriteLine("hitPointsPlayer = " + hitPointsPlayer);
                Console.WriteLine("hitPointsBoss = " + hitPointsBoss);
            }
            else
            {
                Console.WriteLine("Ошибка!!!");
                Console.WriteLine("hitPointsPlayer = " + hitPointsPlayer);
                Console.WriteLine("hitPointsBoss = " + hitPointsBoss);
            }

            Console.WriteLine("Это конец");
        }
    }
}
