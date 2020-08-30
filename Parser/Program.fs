// Learn more about F# at http://fsharp.org

open System
open Sql 
open FSharp.Text

[<EntryPoint>]
let main argv =
 
  let x = "   
  SELECT ANL_BEMERKUNG, LST_BEZEICHNUNG
  FROM ANL
  INNER JOIN LST ON LST.LST_ID = ANL.ANL_FS_LST_ID_TYP   
  WHERE ANL_ID > 1000 AND ANL_BEMERKUNG IS Null
  ORDER BY ANL_ID ASC, ANL_BEMERKUNG DESC
  "   
   
  let lexbuf = Lexing.LexBuffer<_>.FromString x
  let y = SQLParser.start SqlLexer.tokenize lexbuf   
  printfn "%A" y   
   
  Console.WriteLine("(press any key)")   
  Console.ReadKey(true) |> ignore
  0