module Equation

type sign = Pl | Mi 

type term =  Var of double * string | Const of double with 
   member this.IsVariable =
       match this with
       | Var (_,_) -> true
       | Const _ -> false

   member this.Coefficient =
      match this with
            | Var (c,_) -> c
            | Const c -> c

   member this.Invert =
      match this with
       | Var (c,v) -> Var(-c,v)
       | Const c -> Const(-c)

   member this.SortCriteria =
       match this with
        | Var (_,v) -> int32(v.[0])
        | Const c -> 123 // bigger -> ascii 

type Side = term List  
type Equation = Side * Side 
type Equations = Equation List

