using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ValidationBuilder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            var personValidator = new Validator<Person>()
                .NotNull("Person must not be null.")
                .MustBeTrue(person => person.Age >= 18, "Age must be 18 or older.")
                .MustBeTrue(person => !string.IsNullOrEmpty(person.Name), "Name must not be empty.")
                .LessThanOrEqual(person => person.Age, 100, "Age must be 100 or younger.");

            var personToValidate = new Person { Name = "", Age = 15 };

            List<ValidationError> validationErrors = personValidator.Validate(personToValidate);

            if (validationErrors.Count == 0)
            {
                MessageBox.Show("Validation successful!");
            }
            else
            {
                foreach (var error in validationErrors)
                {
                    MessageBox.Show($"Property: {error.PropertyName}, Error: {error.ErrorMessage}");
                }
            }
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
