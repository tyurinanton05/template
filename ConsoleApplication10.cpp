#include <iostream>

// Расчет подоходного налога. Это базовый класс.
class AbstractCalculationTax
{
public:
	// Рассчитать подоходный налог с зарплаты Ме
	virtual const double CalculateTaxByWage(const double moneyWage) = 0;
};

// Упрощенный расчет подоходного налога. ПК
class SimpleTaxCalculation : public AbstractCalculationTax
{
public:

	// Рассчитать подоходный налог с зарплаты Мет
	const double CalculateTaxByWage(const double moneyWage)
	{
		const auto tax = (moneyWage / 100) * 13;
		return tax;
	}
};

// Приближенный к реальному расчет подоходного налога. ПК
class RealTaxCalculation : public AbstractCalculationTax
{
protected:
	// количество отработанных рабочих дней.
	unsigned workDays;
	// количество детей.
	unsigned countChildrens;

public:
	
	RealTaxCalculation(const unsigned workDays, const unsigned countChildrens)
	{
		this->workDays = workDays;
		this->countChildrens = countChildrens;
	}

	// Рассчитать подоходный налог с зарплаты Мет
	const double CalculateTaxByWage(const double moneyWage)
	{
		// налоговый вычет за одного ребенка.
		const auto taxDeductionByChildren = 1400;
		// налоговый вычет за всех детей.
		const auto taxDeductionByChildrens = taxDeductionByChildren * countChildrens;

		// количество рабочих дней в месяце.
		const auto workDaysByMonth = 20;
		const auto income = moneyWage / workDaysByMonth * workDays;
		const auto incomeResult = income - taxDeductionByChildrens;
		const auto tax = (incomeResult / 100) * 13;
		return tax;
	}
};

// Информация о налогах.
class TaxContext
{
private:
	AbstractCalculationTax* calcTax;

public:
	// установить стратегию расчета налога. М
	void SetCalculationTax(AbstractCalculationTax* strategy)
	{
		calcTax = strategy;
	}

	// Рассчитать налог. М.
	const auto CalcTax(const double moneyWage)
	{
		return calcTax->CalculateTaxByWage(moneyWage);
	}
};

int main()
{
	using namespace std;

	const auto context = new TaxContext();

	cout << "Tax by money = " << 40000 << endl;
	context->SetCalculationTax(new SimpleTaxCalculation());
	cout << "Simple calculation = " << context->CalcTax(40000) << endl;

	cout << endl;

	cout << "Tax by money = " << 40000 << endl;
	context->SetCalculationTax(new RealTaxCalculation(22, 0));
	cout << "Real calculation = " << context->CalcTax(40000) << endl;

	cin.get();
    return 0;
}

