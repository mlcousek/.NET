namespace PNE06

module MySort =

    let public quickSortFun (l: int list) =
        let rec sort list =
            match list with
            | [] -> []
            | pivot :: tail ->
                let smallerOrEqual = tail |> List.filter (fun x -> x <= pivot)
                let larger = tail |> List.filter (fun x -> x > pivot)
                sort smallerOrEqual @ [pivot] @ sort larger
        sort l

    let public quickSortImp (arr: int array) =
        let swap i j =
            let temp = arr.[i]
            arr.[i] <- arr.[j]
            arr.[j] <- temp

        let rec partition left right pivotIndex =
            let pivotValue = arr.[pivotIndex]
            swap pivotIndex right
            let mutable storeIndex = left
            for i = left to right - 1 do
                if arr.[i] < pivotValue then
                    swap i storeIndex
                    storeIndex <- storeIndex + 1
            swap storeIndex right
            storeIndex

        let rec quicksort left right =
            if left < right then
                let pivotIndex = (left + right) / 2
                let newPivot = partition left right pivotIndex
                quicksort left (newPivot - 1)
                quicksort (newPivot + 1) right

        quicksort 0 (arr.Length - 1)
        arr

module MySet =
    type public Set() =
        let mutable elements = Set.empty

        member this.Add(i: int) =
            elements <- Set.add i elements

        member this.Contains(i: int) =
            Set.contains i elements

        member this.Remove(i: int) =
            elements <- Set.remove i elements

        member this.Size() =
            Set.count elements

        member this.Union(other: Set) =
            let newSet = Set()
            newSet.AddRange (Set.union elements (other.GetElements()))
            newSet

        member this.Intersection(other: Set) =
            let newSet = Set()
            newSet.AddRange (Set.intersect elements (other.GetElements()))
            newSet

        member this.Subtract(other: Set) =
            let newSet = Set()
            newSet.AddRange (Set.difference elements (other.GetElements()))
            newSet

        member this.IsSubsetOf(other: Set) =
            Set.isSubset elements (other.GetElements())

        member private this.AddRange(newElements: Set<int>) =
            elements <- Set.union elements newElements

        member private this.GetElements() =
            elements