open System.Text.RegularExpressions

let timestampFromLine line = 
  let regex = new Regex("\[(.*)\]")
  let timestampMatch = regex.Match line
  int64 (timestampMatch.Groups.[1].Value.Replace("-", "").Replace(" ", "").Replace(":", ""))

let guardIdFromLine line = 
  let regex = new Regex("#(.+?(?=\s))")
  let guardIdMatch = regex.Match line
  int guardIdMatch.Groups.[1].Value

let minutesFromTimestamp (timestamp:int64) = 
  int (timestamp % int64(100))

let isNewGuard (line:string) = 
  line.Contains("Guard")

let isAsleep (line:string) = 
  line.Contains("asleep")

let rec foo guardId asleepAt transformed input = 
  match input with
  | [] -> transformed 
  | line::rest when isNewGuard line -> foo (guardIdFromLine line) asleepAt transformed rest
  | line::rest when isAsleep line -> foo guardId (timestampFromLine line) transformed rest
  | line::rest when asleepAt < (timestampFromLine line) -> foo guardId (asleepAt + int64(1)) (List.append transformed [(guardId, asleepAt)]) input
  | line::rest -> foo guardId asleepAt transformed rest

let puzzleOne input = 
  let f = foo 0 (int64 -1) [] input

  let guard = f |> List.map (fun x -> fst x) |> List.groupBy id |> List.maxBy (fun x -> List.length (snd x)) |> fst
  let minute = f |> List.filter (fun x -> fst x = guard) |> List.groupBy (fun x -> minutesFromTimestamp (snd x)) |> List.maxBy (fun x -> List.length (snd x)) |> fst

  minute * guard

let puzzleTwo input = 
  let bar = foo 0 (int64 -1) [] input |> List.map (fun x -> (fst x, minutesFromTimestamp (snd x))) |> List.groupBy id |> List.maxBy (fun x -> List.length (snd x)) |> fst

  fst bar * snd bar


[<EntryPoint>]
let main argv =
  let input = System.IO.File.ReadLines("./input.txt") |> Seq.sortBy timestampFromLine |> Seq.toList

  printfn "puzzle one %d" (puzzleOne input)
  printfn "puzzle two %d" (puzzleTwo input)

  0