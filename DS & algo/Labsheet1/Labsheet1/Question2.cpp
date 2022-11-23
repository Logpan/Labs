#include <iostream>
#include <algorithm>
#include <vector>

template <typename T>
T sort(T arg1, T arg2) {
	std::sort(arg1, arg2);
}


class Record
{
public:
	Record(int price, int count) {
		price = price;
		count = count;
	}
private:
	int price;
	int count;
};


int main() {
	std::vector<Record> records = { Record(3,10), Record(4, 20) };
}