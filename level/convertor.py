import json

data = json.load(open("foods.json", encoding="utf-8"))
result = "new Food[]{"
for i in data:
    result += (
        '\
    new Food()$l\
    name="{name}",\
    value={value},\
    protein={dbz},\
    vc={vc},\
    vb={vb},\
    cellulose={cellulose},\
    price={price},\
    count={count},\
    tasteSuan={tasteSuan},\
    tasteTian={tasteTian},\
    tasteKu={tasteKu},\
    tasteLa={tasteLa},\
    tasteXian={tasteXian},\
    tasteMa={tasteMa},\
    tasteYou={tasteYou},\
    tasteQingDan={tasteQingDan},\
    $r,\
    '.format(
            name=i["name"],
            value=i["value"],
            dbz=i["bdz"],
            vc=i["vc"],
            vb=i["vb"],
            cellulose=i["cellulose"],
            price=i["price"],
            count=i["count"],
            tasteSuan=i["taste"]["酸"],
            tasteTian=i["taste"]["甜"],
            tasteKu=i["taste"]["苦"],
            tasteLa=i["taste"]["辣"],
            tasteXian=i["taste"]["咸"],
            tasteMa=i["taste"]["麻"],
            tasteYou=i["taste"]["油"],
            tasteQingDan=i["taste"]["清淡"],
        )
        .replace("$l", "{")
        .replace("$r", "}")
        .replace("    ", "")
        .replace(",", ",\n    ")
        .replace("{", "{\n    ")
    )
result += "\n}"
open("result.txt", "w", encoding="utf-8").write(result)
