def find_max_y(points):
  return points[max(points, key=(lambda key: points[key][1]))][1]

def find_max_x(points):
  return points[max(points, key=(lambda key: points[key][0]))][0]

def distance(firstPoint, secondPoint):
  return abs(firstPoint[0] - secondPoint[0]) + abs(firstPoint[1] - secondPoint[1])

def closest_to(point, points):
  distances = [(k, distance(point, v)) for (k,v) in points.items()]
  minimum_distance = min(distances, key=lambda x: x[1])[1]

  return [x[0] for x in distances if x[1] == minimum_distance]

def get_grid_size(points):
  ymax = find_max_y(points)
  xmax = find_max_x(points)

  return max(xmax, ymax)

def remove_ties_from_grid(grid):
  return {k: v for (k,v) in grid.items() if len(v) == 1}

def remove_infinities_from_grid(grid, grid_size):
  infinities = [v[0] for (k,v) in grid.items() if k[0] == 0]
  infinities += [v[0] for (k,v) in grid.items() if k[1] == 0]
  infinities += [v[0] for (k,v) in grid.items() if k[0] == grid_size]
  infinities += [v[0] for (k,v) in grid.items() if k[1] == grid_size]
  infinities = list(set(infinities))

  grid = {k:v for (k,v) in grid.items() if v[0] not in infinities}

  return grid

def get_largest_area_in_grid(grid):
  unique = {}
  for k,v in grid.items():
    if v[0] in unique.keys():
      unique[v[0]] += 1
    else:
      unique[v[0]] = 1

  return max([v for (k,v) in unique.items()])

def problem_1():
  with open("./input.txt") as file:
    lines = [line.strip().split(',') for line in file]
    points = [(int(line[0]), int(line[1])) for line in lines]
    points = dict(zip(range(len(points)), points))

    grid_size = get_grid_size(points)

    grid = {}

    for x in range(grid_size + 1):
      for y in range(grid_size + 1):
        grid[(x,y)] = closest_to((x,y), points)

    grid = remove_ties_from_grid(grid)
    grid = remove_infinities_from_grid(grid, grid_size)

    return get_largest_area_in_grid(grid)

print(problem_1())
