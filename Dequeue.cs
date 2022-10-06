
using System;

public class Deque {
	static readonly int MAX = 100;
	int[] arr;
	int front;
	int rear;
	int size;

	public Deque(int size)
	{
		arr = new int[MAX];
		front = -1;
		rear = 0;
		this.size = size;
	}

	
	bool isFull()
	{
		return ((front == 0 && rear == size - 1)
				|| front == rear + 1);
	}

	bool isEmpty() { return (front == -1); }

	void insertfront(int key)
	{

		
		if (isFull()) {
			Console.WriteLine("Overflow");
			return;
		}

		
		if (front == -1) {
			front = 0;
			rear = 0;
		}

		else if (front == 0)
			front = size - 1;

		else 
			front = front - 1;

	
		arr[front] = key;
	}

	
	void insertrear(int key)
	{
		if (isFull()) {
			Console.WriteLine(" Overflow ");
			return;
		}

		if (front == -1) {
			front = 0;
			rear = 0;
		}

		else if (rear == size - 1)
			rear = 0;


		else
// This code is contributed by aashish1995
			rear = rear + 1;


		arr[rear] = key;
	}


	void deletefront()
	{
		if (isEmpty()) {
			Console.WriteLine("Queue Underflow\n");
			return;
		}

		if (front == rear) {
			front = -1;
			rear = -1;
		}
// This code is contributed by aashish1995front == size - 1)
			front = 0;

		else 
			
			front = front + 1;
	}

	void deleterear()
	{
		if (isEmpty()) {
			Console.WriteLine(" Underflow");
			return;
		}

		if (front == rear) {
			front = -1;
			rear = -1;
		}
		else if (rear == 0)
			rear = size - 1;
		else
			rear = rear - 1;
	}


	int getFront()
	{
		if (isEmpty()) {
			Console.WriteLine(" Underflow");
			return -1;
		}
		return arr[front];
	}

	
	int getRear()
	{

		if (isEm
// This code is contributed by aashish1995pty() || rear < 0) {
			Console.WriteLine(" Underflow\n");
			return -1;
		}
		return arr[rear];
	}

	public static void Main(String[] args)
	{

		Deque dq = new Deque(5);
	
		Console.WriteLine(
			"Insert element at rear end : 5 ");
		dq.insertrear(5);
		Console.WriteLine(
			"insert element at rear end : 10 ");
		dq.insertrear(10);
		Console.WriteLine("get rear element : "
						+ dq.getRear());
		dq.deleterear();
		Console.WriteLine(
			"After delete rear element new rear become : "
			+ dq.getRear());
		Console.WriteLine("inserting element at front end");
		dq.insertfront(15);
		Console.WriteLine("get front element: "
						+ dq.getFront());
		dq.deletefront();
		Console.WriteLine(
			"After delete front element new front become : "
			+ +dq.getFront());
	}
}

