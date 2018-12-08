def find_max_y(points):
  return max(points, key=lambda p: p[0])[0]

def find_max_x(points):
  return max(points, key=lambda p: p[1])[1]

def get_grid_size(points):
  ymax = find_max_y(points)
  xmax = find_max_x(points)

  return max(xmax, ymax)

def distance(firstPoint, secondPoint):
  return abs(firstPoint[0] - secondPoint[0]) + abs(firstPoint[1] - secondPoint[1])

def sum_of_distances(point, points):
  return sum([distance(point,p) for p in points])

def select_sums_less_than(grid, max_sum):
  return {k:v for (k,v) in grid.items() if v < max_sum}

def problem_2():
  with open("./input.txt") as file:
    lines = [line.strip().split(',') for line in file]
    points = [(int(line[0]), int(line[1])) for line in lines]

  grid_size = get_grid_size(points)

  grid = {}

  for x in range(grid_size + 1):
    for y in range(grid_size + 1):
      grid[(x,y)] = sum_of_distances((x,y), points)

  grid = select_sums_less_than(grid, 10000)
  return len(grid)

print(problem_2())