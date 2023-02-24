using Eating_Philosophers;

/*En filosof kan have tre stadier:

Grøn = De spiser!
Gul = De tænker!
Rød = De sulter!

Extra idé, gør sådan at de kan sulte ihjel.*/
Random random= new();
Philosophers[] philosophers = new Philosophers[5];

//Her instantieres filosofferne. -Bemærk forks. De er forskudt for at simulere et rundt bord.
philosophers[0] = new(Table.Fork5, Table.Fork1, "Kenny McCormick", random.Next(3, 10));
philosophers[1] = new(Table.Fork1, Table.Fork2, "Eric Cartmann", random.Next(3, 10));
philosophers[2] = new(Table.Fork2, Table.Fork3, "Kyle Broflovski", random.Next(3, 10));
philosophers[3] = new(Table.Fork3, Table.Fork4, "Stan Marsh", random.Next(3, 10));
philosophers[4] = new(Table.Fork4, Table.Fork5, "Randy Marsh", random.Next(3, 10));

//Her startes en ny tråd for hver filosof. -Bemærk at hver filosof starter med at tænke.
foreach (var item in philosophers)
{
    new Thread(item.Think).Start();
}

Console.ReadKey();