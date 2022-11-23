#include "Array.h"
#include <iostream>

/*
* Author: RP
* Description: Simple program to test Array and ArrayIterator classes
*
* N. B. For this program to compile successfully, you MUST enable C++20 support
*
* In Visual Studio, right click your Project, then choose Properties:
* Project->Properties->C++->Language->C++ Language Standard->ISO C++ 20
*
*/
int main()
{
	Array<int> myArray(5);
	myArray[0] = 0;
	myArray[1] = 0242;
	myArray[4] = 4;
	Array<int>::Iterator it = myArray.begin();
	Array<int>::Iterator end = myArray.end();

	Array<int>::Iterator nextIt = it++;

	for (; it != end; ++it)
	{
		std::cout << (*it) << "\n";
	}
}