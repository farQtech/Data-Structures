// C# program for implement deletion in Heaps 
using System; 

public class deletionHeap 
{ 

	// To heapify a subtree rooted with node i which is 
	// an index in arr[].Nn is size of heap 
	static void heapify(int []arr, int n, int i) 
	{ 
		int largest = i; // Initialize largest as root 
		int l = 2 * i + 1; // left = 2*i + 1 
		int r = 2 * i + 2; // right = 2*i + 2 

		// If left child is larger than root 
		if (l < n && arr[l] > arr[largest]) 
			largest = l; 

		// If right child is larger than largest so far 
		if (r < n && arr[r] > arr[largest]) 
			largest = r; 

		// If largest is not root 
		if (largest != i) 
		{ 
			int swap = arr[i]; 
			arr[i] = arr[largest]; 
			arr[largest] = swap; 

			// Recursively heapify the affected sub-tree 
			heapify(arr, n, largest); 
		} 
	} 

	// Function to delete the root from Heap 
	static int deleteRoot(int []arr, int n) 
	{ 
		// Get the last element 
		int lastElement = arr[n - 1]; 

		// Replace root with first element 
		arr[0] = lastElement; 

		// Decrease size of heap by 1 
		n = n - 1; 

		// heapify the root node 
		heapify(arr, n, 0); 

		// return new size of Heap 
		return n; 
	} 

	/* A utility function to print array of size N */
	static void printArray(int []arr, int n) 
	{ 
		for (int i = 0; i < n; ++i) 
			Console.Write(arr[i] + " "); 

		Console.WriteLine(); 
	} 

	// Driver Code 
	public static void Main() 
	{ 
		// Array representation of Max-Heap 
		// 10 
		// / \ 
		// 5 3 
		// / \ 
		// 2 4 
		int []arr = { 10, 5, 3, 2, 4 }; 
		int n = arr.Length; 
		n = deleteRoot(arr, n); 
		printArray(arr, n); 
	} 
} 
