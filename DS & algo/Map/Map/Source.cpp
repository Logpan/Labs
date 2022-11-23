#include <map>
#include <list>
#include <string>
#include <iostream>

void countWords(std::list < std::string>& list)
{
	std::map<std::string, int>  map;

	for (std::string s : list) {
		map[s]++;
	}

	int keyCount = 0;

	auto it = map.begin();
	auto itEnd = map.end();

	std::string sReturn = "";
	for (; it != itEnd; it++)
	{
		if (it->second == 2)
		{
			if(sReturn == "")
				sReturn += "\"" + it -> first + "\" ";
			else
				sReturn += "and \"" + it -> first + "\" ";
		}
	}

	/*std::string sReturn = "";
	for (auto [key, value] : map)
	{
		if (value == 2)
		{
			if (sReturn == "")
				sReturn += "\"" + key + "\" ";
			else
				sReturn += "and \"" + key + "\" ";
		}
	}*/

	std::cout << sReturn << " is the only word that appears twice.\n";
}

void main() {
	std::list < std::string> list = { "Geeks", "For", "Geeks" };
	std::list < std::string> list2 = { "Tom", "Jerry", "Thomas", "Tom", "Jerry", "Courage", "Tom", "Courage"};
	countWords(list);
	countWords(list2);
}