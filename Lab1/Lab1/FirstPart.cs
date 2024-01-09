namespace Lab1;

public class FirstPart
{

	public int FirstTask(int[] array)
	{
		int counter = 0;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == 0)
			{
				counter++;
			}
		}
		return counter;
	}
	public int SecondTask(int[] array)
	{
		int sum = 0;
		if (array.Length == 0)
		{
			return 0;
		}
		int idx = Array.IndexOf(array, array.Min());
		for (int i = idx; i < array.Length; ++i)
		{
			sum += array[i];
		}
		return sum;
	}
}