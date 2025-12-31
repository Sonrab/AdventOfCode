namespace AoC.Day10;

public static class Helper {

    // Helper function to find all combinations
    // of size r in an array of size n
    public static void CombinationUtil(int ind, int r, List<int> data, 
        List<List<int>> result, int[] arr) {
        int n = arr.Length;

        // If size of current combination is r
        if (data.Count == r) 
        {
            result.Add([.. data]);
            return;
        }

        // If no more elements are left to put in data
        if (ind >= n) 
        {
            return;
        }

        // include the current element
        data.Add(arr[ind]);

        // Recur for next elements
        CombinationUtil(ind + 1, r, data, result, arr);

        // Backtrack to find other combinations
        data.RemoveAt(data.Count - 1);

        // exclude the current element and move to the next
        CombinationUtil(ind + 1, r, data, result, arr);
    }

    // Function to find all combinations of size r
    // in an array of size n
    public static List<List<int>> FindCombinations(int[] arr, int r) 
    {
        var result = new List<List<int>>();
        var data = new List<int>(); // Temporary array to store current combination
        CombinationUtil(0, r, data, result, arr);
        return result;
    }
}