charList = "$赵钱孙李周吴郑王冯陈褚卫蒋沈韩杨朱秦尤许何吕施张孔曹严华金魏陶姜戚谢邹喻柏水窦章云苏潘葛奚范彭郎鲁韦昌马苗凤花方俞任袁柳酆鲍史唐费廉岑薛雷贺倪汤滕殷罗毕郝邬安常乐于时傅皮卞齐康伍余元卜顾孟平黄和穆萧尹姚邵湛汪祁毛禹狄米贝明臧"
results = []
for i in charList:
    for j in charList:
        for k in charList:
            result = (i + j + k).replace("$", "")
            if len(set(result)) > 1:
                results.append(result)
open("npc.txt", "w", encoding="utf8").write("\n".join(results))
