module EquationAnalyzer

open Equation
open FSharpPlus
open FSharpPlus.Data
open FSharpx.Collections
open FSharpx

let SumConstants (side:Side) =
    let terms = side.ToFSharpList()
    let constants = List.filter (fun (t:term) ->  not t.IsVariable  ) terms
    let cons = Equation.Const (List.sumBy  (fun (t:term) -> t.Coefficient ) constants)
    let variabels = List.filter (fun (t:term) -> t.IsVariable ) terms
    cons::variabels
  
let IsolateConstant (side:Side) = 
    let terms = side.ToFSharpList()
    let inverted = List.filter (fun (t:term) -> t.IsVariable ) terms |> List.map (fun (t:term) -> t.Invert) 
    let constants = List.filter (fun (t:term) -> not t.IsVariable ) terms
    (constants,inverted)

let GetStandardForm (eq:Equation) = 
  let (leftSide,rightSide) = eq
  let LeftSide = SumConstants leftSide
  let rightSide = SumConstants rightSide
  let (constants,inverted) = IsolateConstant rightSide
  
  let leftSide = List.concat [LeftSide;inverted] |> List.sortBy (fun (t:term) -> t.SortCriteria)
  let rightSide = if constants.IsEmpty then [Equation.Const 0.0] else constants
  Equation(leftSide,rightSide)

let UnifyEquation (equations:Equations) = //all Equations must have the same number of variable (missing variable x is 0X  )
    
    equations

let GetCoefficients (equation:Equation) = //Equation x+...-y= c where x..y are variables and c is constant
  let (leftSide,rightSide) = equation
  let Coefficients = leftSide |> List.map (fun (t:term) -> t.Coefficient ) 
  let constant =  rightSide |> head |> (fun (t:term) ->  t.Coefficient)
  (Coefficients,constant)

