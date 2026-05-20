module Upgrade

open System
open System.Threading
open Type

let upgrade (state: GameState) = 
    let i = state.WeaponLv
    let requiredGold = i * 150
    let successRate = 100 - 5 * (i - 1)
    let currentGold = state.Gold
    let rand = Random()
    if i = 10 then 
        printfn "Weapon reached Max Level!"
        state
    else
        printfn "Current Gold: %d" currentGold
        printfn "Choose the Action"
        Thread.Sleep(500)
        printfn "1. Upgrade (Required Gold: %d, Success rate: %d%%)" requiredGold successRate
        printfn "2. Quit"
        match Console.ReadLine() with
        | "1" ->
            if currentGold < requiredGold then 
                printfn "You don't have enough Gold"
                state
            else if rand.Next(1, 101) > successRate then
                printfn "Upgrade Failed. You lost %d Gold" requiredGold
                {state with Gold = (state.Gold - requiredGold)}
            else
                printfn "Upgrade Succeeded! Current Weapon Level: %d" (i + 1)
                {state with Gold = (state.Gold - requiredGold); WeaponLv = (state.WeaponLv + 1)}


        | "2" ->
            printfn "Returning to Menu..."
            Thread.Sleep(1000)
            state
        | _ -> 
            printfn "Invalid Input."
            state