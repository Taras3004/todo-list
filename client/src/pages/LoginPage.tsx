import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { authApi } from "../api/authApi";
import { useError } from "../context/ErrorContext";

export const LoginPage = () => {
  const [isLoginMode, setIsLoginMode] = useState(true);

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [isLoading, setIsLoading] = useState(false);

  const navigate = useNavigate();
  const { showError } = useError();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsLoading(true);

    try {
      if (isLoginMode) {
        const response = await authApi.login({ email, password });
        if (response.token) {
          localStorage.setItem("token", response.token);
          navigate("/tasks");
        }
      } else {
        if (password !== confirmPassword) {
          showError("Passwords do not match!");
          setIsLoading(false);
          return;
        }
        await authApi.register({ email, password, confirmPassword });
        alert("Registration successful! Please login.");
        setIsLoginMode(true);
        setPassword("");
        setConfirmPassword("");
      }
    } catch (err: any) {
      console.error(err);
      const msg = err.response?.data
        ? typeof err.response.data === "string"
          ? err.response.data
          : JSON.stringify(err.response.data)
        : "Authentication failed";
      showError(msg);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="flex h-screen items-center justify-center bg-background">
      <div className="w-full max-w-md bg-white p-8 rounded-default shadow-default transition-all duration-300">
        <h2 className="text-2xl font-bold text-center mb-6 text-foreground">
          {isLoginMode ? "Welcome Back" : "Create Account"}
        </h2>

        <form onSubmit={handleSubmit} className="space-y-4">
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Email
            </label>
            <input
              type="email"
              required
              className="w-full p-2 border border-gray-300 rounded focus:outline-none focus:ring-2 focus:ring-primary"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              placeholder="name@example.com"
            />
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Password
            </label>
            <input
              type="password"
              required
              className="w-full p-2 border border-gray-300 rounded focus:outline-none focus:ring-2 focus:ring-primary"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              placeholder="******"
            />
          </div>

          {!isLoginMode && (
            <div className="animate-in fade-in slide-in-from-top-2 duration-300">
              <label className="block text-sm font-medium text-gray-700 mb-1">
                Confirm Password
              </label>
              <input
                type="password"
                required={!isLoginMode}
                className="w-full p-2 border border-gray-300 rounded focus:outline-none focus:ring-2 focus:ring-primary"
                value={confirmPassword}
                onChange={(e) => setConfirmPassword(e.target.value)}
                placeholder="******"
              />
            </div>
          )}

          <button
            type="submit"
            className="w-full justify-center mt-6 bg-card text-card-foreground rounded-default p-2
                 hover:bg-card-highlighted active:bg-card-clicked transition-colors duration-200"
            disabled={isLoading}
          >
            {isLoading ? "Processing..." : isLoginMode ? "Sign In" : "Sign Up"}
          </button>
        </form>

        <div className="mt-6 text-center text-sm text-gray-600">
          <p>
            {isLoginMode
              ? "Don't have an account? "
              : "Already have an account? "}
            <button
              type="button"
              className="text-foreground font-bold hover:underline focus:outline-none"
              onClick={() => {
                setIsLoginMode(!isLoginMode);
                showError("");
              }}
            >
              {isLoginMode ? "Sign Up" : "Sign In"}
            </button>
          </p>
        </div>
      </div>
    </div>
  );
};
