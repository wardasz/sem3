--append

append [] m = m

append (x:xs) m = x:append xs m

--append :: [a] -> [a] -> [a]

--member

member x [] = False

member x (y:ys) = (x == y) || (member x ys)

--member :: Eq t => t -> [t] -> Bool

--reverse

reverse2 [] = []

reverse2 (x:xs) = append (reverse2 xs) [x]

--reverse2 :: [t] -> [t]

--last

last2 (x:[]) = x

last2 (x:xs) = last xs

--last2 :: [a] -> a

--delete

delete x [] = []

delete x (y:ys) = 
  if x==y then ys
  else (append [y] (delete x ys))

--delete :: Eq t => t -> [t] -> [t]

--split

split x l = spl x l [] []

spl x [] l1 l2 = (l1,l2)

spl x (y:ys) l1 l2 = 
  if x<y then spl x ys (y:l1) l2
  else if x>y then spl x ys l1 (y:l2)
  else spl x ys l1 l2

--split :: Ord a => a -> [a] -> ([a], [a])

--split2

split2 x [] = ([],[])

split2 x (y:ys) = 
  if x<y then let (l1,l2) = split x ys in (y:l1,l2)
  else if x>y then let (l1,l2) = split x ys in (l1,y:l2)
  else let (l1,l2) = split x ys in (l1,l2)

--split2 :: Ord a => a -> [a] -> ([a], [a])

--map

map2 f [] = []

map2 f (x:xs) = (f x):(map2 f xs)

--map2 :: (t1 -> t) -> [t1] -> [t]

--map2

map3 f [] [] = []

map3 f (x:xs) (y:ys) = (f x y):(map3 f xs ys)

--map3 :: (t2 -> t1 -> t) -> [t2] -> [t1] -> [t]

--paring

paring [] [] = []

paring (x:xs) (y:ys) = (x,y):(paring xs ys)

--paring :: [t1] -> [t] -> [(t1, t)]
