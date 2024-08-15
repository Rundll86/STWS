charList = "$赵钱孙李周吴郑王"
results = []
for i in charList:
    for j in charList:
        for k in charList:
            result = (i + j + k).replace("$", "")
            if len(set(result)) > 1:
                results.append(result)
open("npc.txt", "w", encoding="utf8").write("\n".join(results))
