module EquationAnalyzer

open Mathe
open FSharpPlus
open FSharpPlus.Data
open FSharpx.Collections
open FSharpx

let ProcessConstants (side:Side) =
    let terms = side.ToFSharpList()
    let constants = List.filter (fun (t:term) -> match t with | Mathe.Const c -> true  | _ -> false ) terms
    let cons = Mathe.Const (List.sumBy  (fun (t:term) -> match t with | Mathe.Const c -> c  | _ -> 0.0 ) constants)
    let variabels = List.filter (fun (t:term) -> match t with | Mathe.Const c -> false  | _ -> true ) terms
    cons::variabels

  

let processRight (side:Side) = 
    let terms = side.ToFSharpList()
    let inverted = List.map (fun (t:term) -> match t with | Mathe.Const c -> Mathe.Const (-1.0 * c )  | Var(c,v) -> Var(-1.0 * c,v) ) terms
    
    
    ( [Mathe.Const 0.0] ,inverted)

let GetStandardForm (eq:Equation) = 
  let (s1,s2) = eq

  let se1 = ProcessConstants s1

  let se2 = ProcessConstants s2
  
  let (side2,sf2) = processRight se2
  
  let side1 = ProcessConstants (List.concat [se1;sf2])
  Equation(side1,side2)


let GetCoefficients (eq:Equation) = 
  let (s1,s2) = eq

  let Coe1 = List.filter (fun (t:term) -> match t with | Var(c,s) ->   true | _ -> false ) s1 |> List.map (fun (t:term) -> match t with | Mathe.Const c ->  c   | Var(c,_) -> c ) 
  let constant = List.filter (fun (t:term) -> match t with | Mathe.Const c -> true  | _ -> false ) s1 |> List.map (fun (t:term) -> match t with | Mathe.Const c ->  c   | Var(c,_) -> c ) 

  (Coe1,constant.[0])

