using Xunit;
using Contact;

namespace ContactTests;

public class ContactTests
{
    [Fact]
    public void Constructor_WithDefaultValues_ShouldSetEmptyStrings()
    {
        var contact = new Contact.Contact();

        Assert.Equal("", contact.GetFName());
        Assert.Equal("", contact.GetLName());
        Assert.Equal("", contact.GetPhone());
        Assert.Equal("", contact.GetEmail());
    }

    [Fact]
    public void Constructor_WithValues_ShouldSetAllFields()
    {
        var contact = new Contact.Contact("Sebastian", "Arroyo", "7871234567", "test@email.com");

        Assert.Equal("Sebastian", contact.GetFName());
        Assert.Equal("Arroyo", contact.GetLName());
        Assert.Equal("7871234567", contact.GetPhone());
        Assert.Equal("test@email.com", contact.GetEmail());
    }

    [Fact]
    public void Setters_ShouldUpdateValues()
    {
        var contact = new Contact.Contact();

        contact.SetFName("Juan");
        contact.SetLName("Rivera");
        contact.SetPhone("7875551111");
        contact.SetEmail("juan@email.com");

        Assert.Equal("Juan", contact.GetFName());
        Assert.Equal("Rivera", contact.GetLName());
        Assert.Equal("7875551111", contact.GetPhone());
        Assert.Equal("juan@email.com", contact.GetEmail());
    }

    [Fact]
    public void ToString_ShouldReturnExpectedFormat()
    {
        var contact = new Contact.Contact("Ana", "Lopez", "7870000000", "ana@email.com");

        string result = contact.ToString();

        Assert.Equal("Contact[fname=Ana, lname=Lopez, phone=7870000000, email=ana@email.com]", result);
    }

    [Fact]
    public void Equals_WithSameValues_ShouldReturnTrue()
    {
        var contact1 = new Contact.Contact("Carlos", "Diaz", "7871112222", "carlos@email.com");
        var contact2 = new Contact.Contact("Carlos", "Diaz", "7871112222", "carlos@email.com");

        Assert.True(contact1.Equals(contact2));
        Assert.True(contact1 == contact2);
        Assert.False(contact1 != contact2);
    }

    [Fact]
    public void Equals_WithDifferentValues_ShouldReturnFalse()
    {
        var contact1 = new Contact.Contact("Carlos", "Diaz", "7871112222", "carlos@email.com");
        var contact2 = new Contact.Contact("Luis", "Diaz", "7871112222", "carlos@email.com");

        Assert.False(contact1.Equals(contact2));
        Assert.False(contact1 == contact2);
        Assert.True(contact1 != contact2);
    }

    [Fact]
    public void Equals_WithNull_ShouldReturnFalse()
    {
        var contact = new Contact.Contact("Maria", "Santos", "7873334444", "maria@email.com");

        Assert.False(contact.Equals(null));
        Assert.False(contact == null);
        Assert.True(contact != null);
    }

    [Fact]
    public void Equals_WithSameReference_ShouldReturnTrue()
    {
        var contact = new Contact.Contact("Pedro", "Ortiz", "7879998888", "pedro@email.com");

        Assert.True(contact.Equals(contact));
    }

    [Fact]
    public void Equals_ObjectWithDifferentType_ShouldReturnFalse()
    {
        var contact = new Contact.Contact("Pedro", "Ortiz", "7879998888", "pedro@email.com");

        Assert.False(contact.Equals("not a contact"));
    }

    [Fact]
    public void EqualityOperator_BothNull_ShouldReturnTrue()
    {
        Contact.Contact? contact1 = null;
        Contact.Contact? contact2 = null;

        Assert.True(contact1 == contact2);
        Assert.False(contact1 != contact2);
    }

    [Fact]
    public void GetHashCode_SameValues_ShouldReturnSameHashCode()
    {
        var contact1 = new Contact.Contact("Laura", "Vega", "7872223333", "laura@email.com");
        var contact2 = new Contact.Contact("Laura", "Vega", "7872223333", "laura@email.com");

        Assert.Equal(contact1.GetHashCode(), contact2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_DifferentValues_ShouldUsuallyReturnDifferentHashCode()
    {
        var contact1 = new Contact.Contact("Laura", "Vega", "7872223333", "laura@email.com");
        var contact2 = new Contact.Contact("Jose", "Vega", "7872223333", "laura@email.com");

        Assert.NotEqual(contact1.GetHashCode(), contact2.GetHashCode());
    }
}