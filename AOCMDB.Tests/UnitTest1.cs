using System;
using NSpec;

namespace AOCMDB.Tests
{
    public class describe_FirstTest : nspec
    {
        void given_the_world_has_not_come_to_an_end()
        {
            it["Hello World should be Hello World"] = () => "Hello World".should_be("Hello World");
        }
    }
}
