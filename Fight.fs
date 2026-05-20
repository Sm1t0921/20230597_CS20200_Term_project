module Fight 

open System
open System.Threading
open Type

let fight (state: GameState) =
    printfn "Choose the Level of Monster"
    [1 .. state.MonsterLv]
    |> List.iter (fun i -> printfn "Lv %d" i)
    let attack = (50 + state.WeaponLv * 75)
    match Int32.TryParse(Console.ReadLine()) with 
    | (true, i)-> 
        printfn "Fight with Lv %d Monster!" i
        printfn "Attack Power : %d" attack
        printfn "HP : %d" (100 * pown 2 (i - 1))
        let rand = Random()
        let (minDmg: float) = 0.8 * float attack
        let (maxDmg: float) = 1.2 * float attack
        Thread.Sleep(500)
        let rec attackOnce remainHP remainAttack=
            printfn "Choose the Action."
            printfn "1. Attack."
            printfn "2. Run"
            match Console.ReadLine() with
            | "1" ->
                let damage = int (minDmg + (maxDmg - minDmg) * rand.NextDouble())
                let remainHealth = Math.Max(0, remainHP - damage)
                printfn "Damage: %d. Remaining HP: %d." damage remainHealth
                Thread.Sleep(200)
                if (remainHealth = 0) then 
                    printfn "You defeated Lv %d Monster." i
                    Thread.Sleep(200)
                    printfn "You earned %d Gold." (100 * i)
                    Thread.Sleep(200)
                    printfn "Current Gold: %d." (state.Gold + 100 * i)
                    if (i = 5) then
                        printfn "Congratulations! You beat the Final Boss!"
                        {state with IsRunning = false}
                    else if (i >= state.MonsterLv) then
                        {state with Gold = state.Gold + 100 * i; MonsterLv = Math.Min(5, state.MonsterLv + 1)}
                    else
                        {state with Gold = state.Gold + 100 * i}
                else if (remainAttack = 1) then
                    Thread.Sleep(200)
                    printfn "Attack Failed."
                    state
                else
                    Thread.Sleep(300)
                    printfn "%d attacks left." (remainAttack - 1)
                    attackOnce remainHealth (remainAttack - 1)
            | "2" -> 
                printfn "You ran away from the monster."
                state
            | _ -> 
                printfn "Invalid Input."
                state
        attackOnce (100 * pown 2 (i-1)) 3
    | _ -> 
        printfn "Invalid Input."
        state

