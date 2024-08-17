import pandas as pd
import json


def parse_taste(taste_str: str):
    taste = {"酸": 0, "甜": 0, "苦": 0, "辣": 0, "咸": 0, "麻": 0, "油": 0, "清淡": 0}
    if "清淡" in taste_str:
        taste["清淡"] = 1
        return taste
    taste_items = taste_str
    used_as_value = False
    for i in range(len(taste_items)):
        if used_as_value:
            used_as_value = False
            continue
        key = taste_items[i]
        value = int(taste_items[i + 1])
        used_as_value = True
        taste[key] = value
    return taste


def csv_to_json(csv_file, json_file):
    df = pd.read_csv(csv_file)
    json_list = []
    for _, row in df.iterrows():
        item = {
            "name": row["名称"],
            "value": row["饱食度"],
            "bdz": row["蛋白质"],
            "vc": row["维生素C"],
            "vb": row["维生素B"],
            "cellulose": row["纤维素"],
            "price": row["单价"],
            "count": row.get("count", 1),
            "taste": parse_taste(row.get("口味", "")),
            "haveAvatar": bool(row["拥有图标"]),
            "classify": row["分类"],
        }
        json_list.append(item)
    with open(json_file, "w", encoding="utf-8") as f:
        json.dump(json_list, f, ensure_ascii=False, indent=4)


csv_file_path = "foods-database.csv"
json_file_path = "foods-generated.json"
csv_to_json(csv_file_path, json_file_path)
