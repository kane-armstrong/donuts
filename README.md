# Donuts

This repository provides a quick sample of how to put middleware around a db context as a means of invoking
pre/post save changes processes, e.g. automatically hydrating an entity with audit info, untracking tracked 
instances of a given type, dispatching events, etc.