#include <iostream>
#include <vector>

template <typename T>
T addNumbers(std::vector <T>& alpha, int iStart, int iEnd, T iValue = T()) {
	T sum = iValue;
	for (; iStart <= iEnd; iStart++) {
		sum += alpha[iStart];
	}
	return sum;
}

int main() {
	std::cout << "Hello";
	std::vector <int> iTab = { 1,2,3 };
	int result = addNumbers<int>(iTab, 0, 2);

	std::cin.get();
}