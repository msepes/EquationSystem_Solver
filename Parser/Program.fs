// Learn more about F# at http://fsharp.org

open System
open FSharp.Text
open MathNet.Numerics.LinearAlgebra

[<EntryPoint>]
let main argv =

 let input = "x+y=10,-y+x=2"
 
 let lexbuf = Lexing.LexBuffer<_>.FromString input

 let uEquations = EquationParser.start EquationLexer.tokenize lexbuf   
 let Equation = List.map EquationAnalyzer.GetStandardForm uEquations
 //TODO: Missing variables -> Exception! all the equations must have the exact number of variables :(
 let Coefficients = List.map EquationAnalyzer.GetCoefficients Equation
 
 let m = matrix ( List.map (fun (ds,_) -> ds) Coefficients)
 let v = vector ( List.map (fun (_,d) -> d) Coefficients)
 
 printfn "%A" (m.Solve v)   
  
 Console.ReadKey(true) |> ignore
 0