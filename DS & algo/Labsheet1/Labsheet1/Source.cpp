#include <iostream>
#include <vector>

template <typename T>
T addNumbers(std::vector <T>& alpha, int iStart, int iEnd, T iValue = T()) {
	T sum = iValue;
	for (; iStart <= iEnd; iStart++)
		sum += alpha[iStart];
	return sum;
}

int main1() {
	std::vector <int> iTab = { 1,2,3 };
	int result = addNumbers<int>(iTab, 0, 2, 4);
	std::cout << result << "\n";
	std::vector <std::string> sTab = { "AA","BB","CC" };
	std::string sResult = addNumbers<std::string>(sTab, 0, 2);
	std::cout << sResult;

	return 1;
}