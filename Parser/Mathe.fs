module Mathe
type symbol =  Var of string
type sign = Pl | Mi 
type term =  Var of double * string | Const of double
type Side = term List  
type Equation = Side * Side 
type Equations = Equation List

