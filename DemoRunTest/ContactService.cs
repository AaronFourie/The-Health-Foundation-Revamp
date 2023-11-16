using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Firebase.Database;

namespace DemoRunTests
{
    public interface IFirebaseClientWrapper
    {
        Task<FirebaseObject<Contact>> PostAsync(string firebasePath, Contact data);
    }

    public class FirebaseClientWrapper : IFirebaseClientWrapper
    {
        private readonly FirebaseClient _firebaseClient;

        public FirebaseClientWrapper(FirebaseClient firebaseClient)
        {
            _firebaseClient = firebaseClient ?? throw new ArgumentNullException(nameof(firebaseClient));
        }

        public async Task<FirebaseObject<Contact>> PostAsync(string firebasePath, Contact data)
        {
            return await _firebaseClient.Child(firebasePath).PostAsync(data);
        }
    }

    public class ContactService
    {
        private readonly IFirebaseClientWrapper _firebaseClientWrapper;

        public ContactService(IFirebaseClientWrapper firebaseClientWrapper)
        {
            _firebaseClientWrapper = firebaseClientWrapper ?? throw new ArgumentNullException(nameof(firebaseClientWrapper));
        }

        public async Task<bool> SubmitContactFormAsync(string name, string email, string number, string message, string userType)
        {
            try
            {
                // Check if any of the fields are empty
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) ||
                    string.IsNullOrWhiteSpace(number) || string.IsNullOrWhiteSpace(message) || string.IsNullOrWhiteSpace(userType))
                {
                    throw new Exception("One or more fields are empty");
                }

                // Validate email format
                if (!IsValidEmail(email))
                {
                    throw new Exception("Invalid email format");
                }

                // Validate phone number format
                if (!IsValidPhoneNumber(number))
                {
                    throw new Exception("Invalid phone number");
                }

                // Additional validation for other fields
                if (!IsValidName(name))
                {
                    throw new Exception("Invalid name format");
                }

                if (!IsValidUserType(userType))
                {
                    throw new Exception("Invalid user type");
                }

                if (!IsValidMessage(message))
                {
                    throw new Exception("Invalid message format");
                }

                // Save to Firebase Realtime Database using the wrapper
                var result = await _firebaseClientWrapper.PostAsync("contacts", new Contact
                {
                    Name = name,
                    Email = email,
                    Number = number,
                    Message = message,
                    UserType = userType
                });

                return result?.Object != null;
            }
            catch (Exception ex)
            {
                // Log the error or handle it accordingly
                Console.WriteLine($"Error submitting contact form: {ex.Message}");
                return false;
            }
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$");
        }

        private bool IsValidPhoneNumber(string number)
        {
            return Regex.IsMatch(number, @"^[0-9]{10}$"); // Example: 10-digit number validation
        }

        private bool IsValidName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                string[] nameParts = name.Trim().Split(' ');
                return nameParts.Length >= 2;
            }
            return false;
        }

        private bool IsValidUserType(string userType)
        {
            string[] validUserTypes = { "Individual", "Business" };
            return !string.IsNullOrWhiteSpace(userType) && validUserTypes.Contains(userType);
        }

        private bool IsValidMessage(string message)
        {
            return !string.IsNullOrWhiteSpace(message) && message.Length >= 10;
        }
    }

    public class Contact
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public string Message { get; set; }
        public string UserType { get; set; }
    }
}