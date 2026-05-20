open System
open System.Threading
open Type
open Fight
open Upgrade

let rec gameLoop (state: GameState) = 
    if (state.IsRunning = false) then
        printfn "Thanks for Playing!"
    else
        printfn "Gold: %d" state.Gold
        printfn "Choose the Action."
        printfn "1. Fight."
        printfn "2. Upgrade."
        printfn "3. Exit."
        
        match Console.ReadLine() with
        | "1" -> 
            let fightState = fight state
            gameLoop fightState
        | "2" -> 
            let upgradeState = upgrade state
            gameLoop upgradeState
        | "3" -> 
            printfn "Exiting..."
            Thread.Sleep(1000)
            let exitState = { state with IsRunning = false}
            gameLoop exitState
        | _ -> 
            printfn "Invalid Input."
            gameLoop state


[<EntryPoint>]
let main argv =
    let initialState = { 
        IsRunning = true 
        Gold = 0
        MonsterLv = 1
        WeaponLv = 1
    }
    printfn "Defeat the Monster!"
    Thread.Sleep(1000)
    printfn "Loading..."
    Thread.Sleep(3000)
    gameLoop initialState

    0