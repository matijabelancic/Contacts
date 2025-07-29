import axios from "axios";

// Initialize base Axios instance
const apiClient = axios.create({
    baseURL: "https://localhost:7157/",
    headers: {
        "Content-Type": "application/json",
    },
    withCredentials: false,
});


export const fetchContacts = async (
    pageNumber = 1,
    pageSize = 10,
    searchTerm = '',
    sortBy = '',
    sortByAscending = true) => {
    try {
        const response = await apiClient.get(`Contacts`, {
            params: {
                PageNumber: pageNumber,
                PageSize: pageSize,
                SearchTerm: searchTerm || undefined,
                SortBy: sortBy || undefined,
                SortByAscending: sortByAscending
            },
        });
        return response.data;
    } catch (error) {
        throw new Error(error.response?.data || "Error fetching contacts");
    }
};

export const fetchContactById = async (id) => {
    try {
        const response = await apiClient.get(`Contacts/${id}`);
        return response.data;
    } catch (error) {
        throw new Error(error.response?.data || "Error fetching contact by ID");
    }
};

export const updateContact = async (id, updatedContact) => {
    try {
        const response = await apiClient.put(`Contacts/${id}`, updatedContact);
        return response.data;
    } catch (error) {
        throw new Error(error.response?.data || "Error updating contact");
    }
};

export async function createContact(contactData) {
    const response = await fetch('https://localhost:7157/Contacts', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(contactData),
    });

    if (!response.ok) {
        throw new Error(`Failed to create contact: ${response.statusText}`);
    }

    return response.json(); // Parse and return the response data (newly created contact)
}

export async function deleteContact(id) {
    const response = await fetch(`https://localhost:7157/Contacts/${id}`, {
        method: 'DELETE',
    });

    if (response.status !== 204) {
        throw new Error(`Failed to delete contact: ${response.statusText}`);
    }

    return response;
}


