namespace Project.Core
{
    using UnityEngine;
    using System.Text.RegularExpressions;

    [System.Serializable]
    public class UsernameService : MonoBehaviour
    {
        [Header("Username Configuration")]
        [SerializeField] private string usernamePrefix = "KHAMSA_";
        [SerializeField] private int randomDigits = 5;
        [SerializeField] private int maxUsernameLength = 12;
        
        private string username;
        public event System.Action<string> OnUsernameChanged;
        
        public enum ValidationResult
        {
            Valid,
            TooLong,
            ContainsArabic,
            AlreadyTaken,
            Empty
        }
        
        public void Init()
        {
            username = PlayerPrefs.GetString("Username", "");
            if (string.IsNullOrEmpty(username))
            {
                GenerateRandomUsername();
            }
            Debug.Log($"UsernameService initialized. Username: {username}");
        }
        
        private void GenerateRandomUsername()
        {
            int randomNumber = UnityEngine.Random.Range(
                (int)Mathf.Pow(10, randomDigits - 1), 
                (int)Mathf.Pow(10, randomDigits) - 1
            );
            username = usernamePrefix + randomNumber;
            PlayerPrefs.SetString("Username", username);
        }
        
        public string GetUsername() => username;
        
        public ValidationResult ValidateUsername(string newUsername)
        {
            // Check if empty
            if (string.IsNullOrEmpty(newUsername.Trim()))
            {
                return ValidationResult.Empty;
            }
            
            // Check length
            if (newUsername.Length > maxUsernameLength)
            {
                return ValidationResult.TooLong;
            }
            
            // Check for Arabic characters
            if (ContainsArabic(newUsername))
            {
                return ValidationResult.ContainsArabic;
            }
            
            // Check if already taken (TODO: implement server check)
            if (IsUsernameTaken(newUsername))
            {
                return ValidationResult.AlreadyTaken;
            }
            
            return ValidationResult.Valid;
        }
        
        public bool SetUsername(string newUsername)
        {
            ValidationResult result = ValidateUsername(newUsername);
            
            if (result == ValidationResult.Valid)
            {
                username = newUsername.Trim();
                PlayerPrefs.SetString("Username", username);
                OnUsernameChanged?.Invoke(username);
                Debug.Log($"Username changed to: {username}");
                return true;
            }
            
            Debug.Log($"Username validation failed: {result}");
            return false;
        }
        
        private bool ContainsArabic(string text)
        {
            // Arabic Unicode range: U+0600 to U+06FF
            return Regex.IsMatch(text, @"[\u0600-\u06FF]");
        }
        
        private bool IsUsernameTaken(string username)
        {
            // TODO: Implement server check
            // For now, simulate some taken usernames
            string[] takenUsernames = { "admin", "test", "khamsa", "game" };
            return System.Array.Exists(takenUsernames, u => u.ToLower() == username.ToLower());
        }
        
        public string GetValidationMessage(ValidationResult result)
        {
            switch (result)
            {
                case ValidationResult.TooLong:
                    return $"اسم المستخدم طويل جداً (الحد الأقصى {maxUsernameLength} حرف)";
                case ValidationResult.ContainsArabic:
                    return "يجب استخدام أحرف إنجليزية فقط";
                case ValidationResult.AlreadyTaken:
                    return "اسم المستخدم محجوز بالفعل";
                case ValidationResult.Empty:
                    return "اسم المستخدم لا يمكن أن يكون فارغ";
                case ValidationResult.Valid:
                    return "اسم المستخدم صالح";
                default:
                    return "خطأ غير معروف";
            }
        }
        
        public int GetMaxLength() => maxUsernameLength;
        
        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}