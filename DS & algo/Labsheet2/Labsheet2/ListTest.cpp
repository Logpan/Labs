#include <iostream>
#include "SLinkedList.h"

int main()
{
	// Sample usage of SLinkedList class. 

	// Create a list with 3 nodes.
	SLinkedList<int> list;
	list.insertFirst(20);
	list.insertFirst(10);
	list.insertFirst(5);
	list.insertFirst(5);
	list.insertFirst(10);

	for (int item : list)
	{
		//std::cout << "test";
	}

	// Get an iterator to list beginning and "one past the end"
	SLinkedList<int>::Iterator it = list.begin();
	SLinkedList<int>::Iterator itEnd = list.end();

	// Simple equality test on two iterators
	bool result = (it != itEnd);

	// Output the value of the first list element
	std::cout << "First element: " << *it << std::endl;

	std::cout << "Try to find 10 : " << *list.find(10) << "\n";

	int t_target;
	std::cin >> t_target;


	std::cout << "Try to find " << t_target << " : ";

	SLinkedList<int>::Iterator resultIt = list.find(t_target);
	list.insertBefore(resultIt, 8);
	if (resultIt == itEnd)
	{
		std::cout << "Not Found !\n";
	}
	else
	{
		std::cout << "Found !\n";
	}

	list.moveLastToFront();
	//list.unique();
	//auto i = list.begin();
	// Output the number of nodes in the list
	std::cout << "Size : " << list.size() << std::endl;

	system("PAUSE");
}
