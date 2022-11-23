#include <iostream>
#include <vector>

void main() {
	std::vector<int> gamesScore;
	//Short exeriment to test vector size and capacity
	std::vector<int> nums;
	//nums.reserve(200);
	std::cout << nums.size() << "\t" << nums.capacity() << "\n";
	for (int i = 0; i < 200; i++)
	{
		nums.emplace_back(i);
		if (nums.capacity() - nums.size() == 0)
		{
			std::cout << "Reallocating memory\n";
			std::cout << nums.size() << "\t" << nums.capacity() << "\n";
		}
	}
	nums.shrink_to_fit();
	//nums.clear();
}