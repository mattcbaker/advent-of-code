defmodule Problem1 do
  def dependents(input) do
    input |> Enum.map(fn x -> elem(x, 1) end)
  end

  def parents(input) do
    input |> Enum.map(fn x -> elem(x, 0) end)
  end

  def khan(input, order, steps) do
    if length(input) < 1 do
      remaining = steps |> Enum.filter(fn x -> !Enum.member?(order, x) end) |> Enum.sort()
      order = order ++ remaining
      Enum.join(order, "")
    else
      {parent, child} = nextRoot(input)

      order = if !Enum.member?(order, parent), do: order ++ [parent], else: order

      input = removeRoot(input, {parent, child})

      khan(input, order, steps)
    end
  end

  def unique(input) do
    parents(input) ++ dependents(input) |> Enum.uniq()
  end

  def nextRoot(input) do
    Enum.filter(input, fn x -> !Enum.member?(dependents(input), elem(x, 0)) end) |> Enum.sort() |> Enum.at(0)
  end

  def removeRoot(input, root) do
    Enum.filter(input, fn x -> x != root end)
  end
end

input =
  File.stream!("./input.txt")
  |> Stream.map(&String.trim/1)
  |> Stream.map(&(String.split(&1, " ")))
  |> Stream.map(&({Enum.at(&1, 1), Enum.at(&1, 7)}))
  |> Enum.to_list()

  khan = Problem1.khan(input, [], Problem1.unique(input))
  IO.inspect(khan)