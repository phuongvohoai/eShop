export const BaseApi = {
  sendRequest(url: string, init?: RequestInit): Promise<Response> {
    return fetch(`${process.env.BASE_API_URL}/${url}`, {
      ...init,
      headers: {
        ...init?.headers,
        'Content-Type': 'application/json'
      },

    });
  }
}
