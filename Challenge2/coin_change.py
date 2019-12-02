def change_coins(coins, amount):
    max = amount + 1
    result = [max] * max
    coinsResults = [[] for _ in range(max)]  #initialize empty array with max rows
    result[0] = 0
    print(coinsResults)
    for i in range(1,max):
        for coin in coins:
            if i >= coin and (result[i - coin] + 1 < result[i]) :
                result[i] = result[i-coin] + 1
                coinsResults[i] = coinsResults[i-coin] + [coin]
    if result[amount] == max:
        return "not possible"

    resultDict = {}
    for i in coinsResults[amount]:
        if str(i) in resultDict:
            resultDict[str(i)] += 1
        else:
            resultDict[str(i)] = 1

    return resultDict


print(change_coins([1,7,10],17))