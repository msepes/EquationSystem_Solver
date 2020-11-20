// Learn more about F# at http://fsharp.org

open System
open FSharp.Text
open MathNet.Numerics.LinearAlgebra

[<EntryPoint>]
let main argv =

 let input = "x+y=10,x-y=0"
 
 let lexbuf = Lexing.LexBuffer<_>.FromString input

 let uEquations = EquationParser.start EquationLexer.tokenize lexbuf   
 let Equation = List.map EquationAnalyzer.GetStandardForm uEquations

 let cos = List.map EquationAnalyzer.GetCoefficients Equation
 
 let m = matrix ( List.map (fun (ds,_) -> ds) cos)
 let v = vector ( List.map (fun (_,d) -> -1.0*d) cos)
 
 printfn "%A" (m.Solve v)   
  
 Console.ReadKey(true) |> ignore
 0