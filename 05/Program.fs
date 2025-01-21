printfn "Hello from F#"

// 1

let rec gcd a b =
    if b = 0 then a
    else gcd b (a % b)

// 1 test

printfn "gcd 54 24 = %d" (gcd 54 24)  // Výstup: 6
printfn "gcd 17 13 = %d" (gcd 17 13)  // Výstup: 1
printfn "gcd 100 25 = %d" (gcd 100 25)  // Výstup: 25


printfn "\n"
// 2

type Fraction = int * int

// Normalizace zlomku do základního tvaru
let normalize (num, denom) =
    let d = gcd (abs num) (abs denom)
    (num / d, denom / d)

// Sčítání zlomků
let addFrac (n1, d1) (n2, d2) =
    let num = n1 * d2 + n2 * d1
    let denom = d1 * d2
    normalize (num, denom)

// Odčítání zlomků
let subFrac (n1, d1) (n2, d2) =
    let num = n1 * d2 - n2 * d1
    let denom = d1 * d2
    normalize (num, denom)

// Násobení zlomků
let mulFrac (n1, d1) (n2, d2) =
    let num = n1 * n2
    let denom = d1 * d2
    normalize (num, denom)

// Dělení zlomků
let divFrac (n1, d1) (n2, d2) =
    let num = n1 * d2
    let denom = d1 * n2
    normalize (num, denom)

// 2 testy

let frac1 = (1, 2)  // 1/2
let frac2 = (1, 3)  // 1/3

// Testování sčítání zlomků
let (n, d) = addFrac frac1 frac2
printfn "addFrac (1/2) + (1/3) = %d/%d" n d  // Výstup: 5/6

// Testování odčítání zlomků
let (n1, d1) = subFrac frac1 frac2
printfn "subFrac (1/2) - (1/3) = %d/%d" n1 d1  // Výstup: 1/6

// Testování násobení zlomků
let (n2, d2) = mulFrac frac1 frac2
printfn "mulFrac (1/2) * (1/3) = %d/%d" n2 d2  // Výstup: 1/6

// Testování dělení zlomků
let (n3, d3) = divFrac frac1 frac2
printfn "divFrac (1/2) / (1/3) = %d/%d" n3 d3  // Výstup: 3/2


printfn "\n"
// 3

let rec factorial n =
    if n = 0 then 1
    else n * factorial (n - 1)

let comb n k =
    factorial n / (factorial k * factorial (n - k))

// 3 testy
printfn "comb 5 2 = %d" (comb 5 2)  // Výstup: 10
printfn "comb 6 3 = %d" (comb 6 3)  // Výstup: 20
printfn "comb 10 4 = %d" (comb 10 4)  // Výstup: 210


printfn "\n"
// Stromy

type Tree<'T> =
    | Empty
    | Node of 'T * Tree<'T> * Tree<'T>

let rec sumOfTree tree =
    match tree with
    | Empty -> 0
    | Node(value, left, right) ->
        value + sumOfTree left + sumOfTree right

let rec leavesToList tree =
    match tree with
    | Empty -> []
    | Node(value, Empty, Empty) -> [value]
    | Node(_, left, right) -> leavesToList left @ leavesToList right

// Stromy testy

let tree =
    Node(5,
         Node(3, Node(1, Empty, Empty), Node(4, Empty, Empty)),
         Node(8, Node(6, Empty, Empty), Node(10, Empty, Empty)))

// Testování součtu hodnot stromu
printfn "sumOfTree = %d" (sumOfTree tree)  // Výstup: 37

// Testování listů stromu
let leaves = leavesToList tree
printfn "leavesToList = %A" leaves  // Výstup: [1; 4; 6; 10]
