import UnityPy

path = r"C:\Program Files (x86)\Steam\steamapps\common\Dyson Sphere Program\DSPGAME_Data\resources.assets"
env = UnityPy.load(path)
objects = {obj.path_id: obj for obj in env.objects}


def get_transform(go_id):
    for comp_ref in objects[go_id].read().m_Components:
        comp = objects.get(comp_ref.path_id)
        if comp and comp.type.name == "Transform":
            return comp.read()
    return None


def dump_go(path_id, indent=0):
    obj = objects.get(path_id)
    if not obj or obj.type.name != "GameObject":
        return 0

    data = obj.read()
    print("  " * indent + f"GO {data.m_Name} ({path_id})")
    child_count = 0

    for comp_ref in data.m_Components:
        comp = objects.get(comp_ref.path_id)
        if not comp:
            continue
        ctype = comp.type.name
        print("  " * indent + f"  [{ctype}]")
        if ctype == "MonoBehaviour":
            try:
                tree = comp.read_typetree()
                for key in sorted(tree.keys()):
                    if key.startswith("m_"):
                        continue
                    val = tree[key]
                    if val not in (None, "", 0, False):
                        print("  " * indent + f"    {key} = {val!r}")
            except Exception as ex:
                print("  " * indent + f"    unreadable: {ex}")

    transform = get_transform(path_id)
    if transform:
        for child in transform.m_Children:
            child_count += 1 + dump_go(child.path_id, indent + 1)
    return child_count


best = None
best_children = -1
for obj in env.objects:
    if obj.type.name != "GameObject" or obj.read().m_Name != "type-button":
        continue
    children = 0
    transform = get_transform(obj.path_id)
    if transform:
        children = len(transform.m_Children)
    if children > best_children:
        best_children = children
        best = obj.path_id

if best is None:
    print("No type-button found")
else:
    print(f"Dumping type-button {best} with {best_children} direct children")
    dump_go(best)