var c = new[] { '1', '2', '3', '4', '5', '6' };
var s = new string(c);

var res = SW(s);
System.Console.WriteLine(res);

string SW(string ass) => ass switch
{
    string { Length: > 5 } => "a" 
};