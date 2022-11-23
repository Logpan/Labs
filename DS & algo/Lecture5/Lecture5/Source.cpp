#include <vector>
#include <algorithm>
#include <ranges>

int main()
{
	std::vector<std::string>vecOfStrs = { "Hi", "Hello", "Test", "first", "second", "third", "fourth"};

	bool res = std::ranges::any_of(vecOfStrs,
		[](std::string& t_str)
		{
			return t_str.size() == 4;
		});
}