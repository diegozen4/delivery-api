namespace Contracts.Auth;

public record ChangePasswordRequest(
    string OldPassword,
    string NewPassword,
    string ConfirmNewPassword
);
