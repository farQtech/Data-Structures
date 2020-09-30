using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSTest
{
  class SLinkCircularList

  {
    private int data;
    private SLinkCircularList next;
    public SLinkCircularList(int value)
    {
      data = value;
      next = this;
    }

    public SLinkCircularList InsertNext(int value)
    {
      SLinkCircularList node = new SLinkCircularList(value);
      if (this.next == this)
      {
        node.next = this;
        this.next = node;
      }
      else
      {
        SLinkCircularList temp = this.next;
        node.next = temp;
        this.next = node;
      }
      return node;
    }

    public int DeleteNext()
    {
      if (this.next == this)
      {
        System.Console.WriteLine("\nThe node can not be deleted as there is only one node in the circular list");
        return 0;
      }
      SLinkCircularList node = this.next;
      this.next = this.next.next;
      node = null;
      return 1;
    }

    public void Traverse(SLinkCircularList node)
    {
      if (node == null)
      node = this;
      System.Console.WriteLine("\n\nTraversing in Forward Direction\n\n");
      SLinkCircularList startnode = node;
    do
      {
        System.Console.WriteLine(node.data);
        node = node.next;
      }
      while (node != startnode);
    }
    static void Main(string[] args)
    {
      SLinkCircularList node1 = new SLinkCircularList(1);
      node1.DeleteNext();
      SLinkCircularList node2 = node1.InsertNext(2);
      node1.DeleteNext();
      node2 = node1.InsertNext(2);
      SLinkCircularList node3 = node2.InsertNext(3);
      SLinkCircularList node4 = node3.InsertNext(4);
      SLinkCircularList node5 = node4.InsertNext(5);
      node1.Traverse(node1);
      node3.DeleteNext(); 
      node2.Traverse(node2);
    }
  }

}
