using Eating_Philosophers;

/*
En filosof kan have tre stadier:

Grøn = De spiser!
Gul = De tænker!
Rød = De sulter!

Extra idé, gør sådan at de kan sulte ihjel.
 */
Random random= new();

//Her instantieres filosofferne. -Bemærk forks. De er forskudt for at simulere et rundt bord.
Philosophers kennyM = new(Table.Fork5, Table.Fork1, "Kenny McCormick", random.Next(3,10));
Philosophers ericC = new(Table.Fork1, Table.Fork2, "Eric Cartmann", random.Next(3, 10));
Philosophers kyleB = new(Table.Fork2, Table.Fork3, "Kyle Broflovski", random.Next(3, 10));
Philosophers stanM = new(Table.Fork3, Table.Fork4, "Stan Marsh", random.Next(3, 10));
Philosophers randyM = new(Table.Fork4, Table.Fork5, "Randy Marsh", random.Next(3, 10));

//Her startes en ny tråd for hver filosof. -Bemærk at hver filosof starter med at tænke.
new Thread(kennyM.Think).Start();
new Thread(ericC.Think).Start();
new Thread(kyleB.Think).Start();
new Thread(stanM.Think).Start();
new Thread(randyM.Think).Start();

Console.ReadKey();