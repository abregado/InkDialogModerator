-> first_knot

== first_knot ==
#camera both
#speaker Bob
Hi this is Bob
#speaker Sarah
and this is Sarah
#speaker Bob
#color cube
The Cube should be blue now. 
#color cube
#speaker Sarah
I prefer it to be grey.
* [I want it to be Blue] -> choice_one
* [Leave it grey] -> choice_two

==choice_one==
#color Cube
#speaker Bob
Thanks for agreeing with me.
-> second_knot


==choice_two==
#speaker Sarah
Thanks for agreeing with me.
-> second_knot

==second_knot==
#speaker Bob
#camera sphere
Don't ask about the sphere.
#speaker Sarah
Yeah that belongs to Rachael.
We don't touch it.
#speaker Racheal
I prefer it to change colors.
#color sphere
Sometimes blue...
#color sphere
Sometimes grey.
Since there are no more shapes
#camera both
This conversation is over.
-> END