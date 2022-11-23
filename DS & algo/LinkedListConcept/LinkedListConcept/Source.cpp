#include <iostream>
#include <memory> // for std::unique_ptr

// Program to demonstrate basics of std::unique_ptr using a Node class

// A Node class that can only handle integers
class Node
{
public:
	Node(int t_val) : m_val(t_val) {}

	void setNext(std::unique_ptr<Node>& t_next)
	{
		m_next.swap(t_next);
	}

	std::unique_ptr<Node>& next()
	{
		return m_next;
	}

	int element() const
	{
		return m_val;
	}

private:
	int m_val;
	std::unique_ptr<Node> m_next;
};

//-----------------------------------------------------------
// Exercise 1: Complete this function so that it creates
//  a new node with value t_param and adds it to the 
//  end of the list. The function should return a pointer
//  to the new tail of the list.
//-----------------------------------------------------------
Node* append(Node* t_tail, int t_param)
{
	std::unique_ptr<Node> temp = std::make_unique<Node>(t_param);
	t_tail->setNext(temp);
	return t_tail->next().get();
}

//-----------------------------------------------------------
// Exercise 2: Complete this function so that a new node
//  with value t_param is inserted into the list, where the 
//  list elements are maintained in ascending sequence.
//-----------------------------------------------------------
void insert(Node* t_head, int t_param)
{
	// Example: if the list contains:
	// 1->3->5
	// Then insert(4) will result in the following list: 
	// 1->3->4->5
	// Hints: 
	// 1. Loop to find the insertion position (which might be the at the very list end)
	// 2. Once insertion position is found, check if the new node is to be appended 
	//    to the rear or inserted somewhere in the middle.

	Node* current;
	Node* previous = null;

	for (current = t_head; current != null && current.element() < t_param; current = current->next().get())
		previous = current;
	if (previous != null)
	{
		// Create a new nodes
		std::unique_ptr<Node> temp = std::make_unique<Node>(t_param);

		// Make new node point to successor of previous
		temp->setNext(previous->next());
		previous->setNext(temp);
	}
}


int main()
{
	//-----------------------------------------------------------
	// Dynamic creation of a Node object with std::make_unique
	std::unique_ptr<Node> n1 = std::make_unique<Node>(1);
	std::unique_ptr<Node> n2;
	// Assignment from n2 to n1 not possible, but we can swap
	n1.swap(n2);		// Note n1 is now empty.

	//-----------------------------------------------------------
	// Make a list and point n1 at n2
	n1 = std::make_unique<Node>(1);
	n2 = std::make_unique<Node>(2);
	n1->setNext(n2);	// Note that Node::setNext() implementation calls swap

	// To get the successor of node n1 (note: variable successor *must* be a reference)
	std::unique_ptr<Node>& successor = n1->next();

	// With references - can be initialised only, can never be reassigned
	std::unique_ptr<Node>& nodeRef = n1;
	// Reference cannot be reassigned
	// nodeRef = n2;

	// We can get a raw pointer from a std::unique_ptr like this:
	Node* first = n1.get();

	std::cin.get();
}
